using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Search
{
    class clsGetInvoiceNumber
    {
        /// <summary>
        /// create db from the data access class
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// variable to hold the SQL statement
        /// </summary>
        private string sSQL = clsSearchSQL.search();

        //list to hold invoice data
        public List<Invoice> numberList = new List<Invoice>();

        public class Invoice
        {
            /// <summary>
            /// invoice attributes
            /// </summary>
            public string number { get; set; }

            /// <summary>
            /// override the ToString() method to return the invoice number
            /// </summary>
            /// <returns>a string of the invoice number</returns>
            public override string ToString()
            {
                try
                {
                    //return info
                    return "#" + number;
                }
                catch (Exception ex)
                {
                    //Just throw the exception
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        /// <summary>
        /// method to connect to dataset, use query and add invoices to the invoice list
        /// </summary>
        public void getInvoiceNumber()
        {
            try
            {
                try
                {
                    //clear the passenger list so that we don't add duplicates
                    numberList.Clear();

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

                        numberList.Add(invoice);
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
