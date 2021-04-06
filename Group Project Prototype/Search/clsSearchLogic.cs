using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Search
{
    class clsSearchLogic
    {   
        /// <summary>
        /// variable to hold the SQL statement
        /// </summary>
        private string sSQL = "";

        //list to hold invoice data?
        //public static List<Invoice> passengerList = new List<Invoice>();

        /// <summary>
        /// variables to hold filter data
        /// </summary>
        private int cost;
        private int invocieNumber;
        private DateTime dateStart;
        private DateTime dateEnd;

        /// <summary>
        /// variables to reflect if the specified filters are selected
        /// </summary>
        private bool costSelected;
        private bool invoiceNumberSelected;
        private bool dateStartSelected;
        private bool dateEndSelected;

        /// <summary>
        /// method to create the query based on the filter selections
        /// </summary>
        public void createQuery()
        {
            try
            {
                if (!(costSelected && invoiceNumberSelected && dateStartSelected && dateEndSelected))
                {
                    sSQL = clsSearchSQL.search();
                }
                //continue down with selection cases
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method to conenct to dataset, use query and add invoices to the invoice list
        /// </summary>
        public static void getInvoiceData()
        {
            try
            {
                
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
