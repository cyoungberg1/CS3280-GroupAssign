using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// create db from the data access class
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// variable to hold the SQL statement
        /// </summary>
        private string sSQL = "";

        //list to hold invoice data
        public List<Invoice> invoiceList = new List<Invoice>();
        public List<Invoice> numberList = new List<Invoice>();
        public List<Invoice> costList = new List<Invoice>();

        /// <summary>
        /// variables to hold filter data
        /// </summary>
        public string cost { get; set; }
        public string invoiceNumber { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }

        /// <summary>
        /// variables to reflect if the specified filters are selected
        /// </summary>
        public bool costSelected { get; set; }
        public bool invoiceNumberSelected { get; set; }
        public bool dateStartSelected { get; set; }
        public bool dateEndSelected { get; set; }

        /// <summary>
        /// simple passenger class to hold passenger data and override the ToString() method
        /// </summary>
        public class Invoice
        {
            /// <summary>
            /// invoice attributes
            /// </summary>
            public string number { get; set; }
            public string date { get; set; }
            public string amount { get; set; }
        }

        /// <summary>
        /// method to create the query based on the filter selections
        /// </summary>
        public void createQuery()
        {
            try
            {
                if (!costSelected && !invoiceNumberSelected && !dateStartSelected && !dateEndSelected)
                {
                    sSQL = clsSearchSQL.search();
                }
                else if (costSelected && !invoiceNumberSelected && !dateStartSelected && !dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchCost(cost);
                }
                else if (!costSelected && invoiceNumberSelected && !dateStartSelected && !dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchNum(invoiceNumber);
                }
                else if (!costSelected && !invoiceNumberSelected && dateStartSelected && dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchDate(dateStart, dateEnd);
                }
                else if (costSelected && invoiceNumberSelected && !dateStartSelected && !dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchCostNum(cost, invoiceNumber);
                }
                else if (costSelected && !invoiceNumberSelected && dateStartSelected && dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchDateCost(dateStart, dateEnd, cost);
                }
                else if (!costSelected && invoiceNumberSelected && dateStartSelected && dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchDateNum(dateStart, dateEnd, invoiceNumber);
                }
                else if (costSelected && invoiceNumberSelected && dateStartSelected && dateEndSelected)
                {
                    sSQL = clsSearchSQL.searchAll(dateStart, dateEnd, cost, invoiceNumber);
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method to connect to dataset, use query and add invoices to the invoice list
        /// </summary>
        public void getInvoiceData()
        {
            try
            {
                try
                {
                    createQuery();
                    //clear the invoice list so that we don't add duplicates
                    invoiceList.Clear();

                    int iRet = 0;   //Number of return values
                    DataSet ds = new DataSet(); //Holds the return values
                    Invoice invoice; //Used to load the return values into the collection

                    //Extract the passengers and put them into the DataSet
                    ds = db.ExecuteSQLStatement(sSQL, ref iRet);


                    //foreach()
                    //Loop through the data and create passenger classes, adding them to the passengerList
                    for (int i = 0; i < iRet; i++)
                    {
                        invoice = new Invoice();
                        invoice.number = ds.Tables[0].Rows[i][0].ToString();
                        DateTime dateTime = (DateTime)ds.Tables[0].Rows[i][1];
                        invoice.date = dateTime.ToString("MM/dd/yyyy");
                        invoice.amount = ds.Tables[0].Rows[i][2].ToString();

                        invoiceList.Add(invoice);
                    }
                }
                catch (Exception ex)
                {
                    //Just throw the exception
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
