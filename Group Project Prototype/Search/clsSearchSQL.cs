using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// set query to search all
        /// </summary>
        public static string search()
        {
            try
            {
                return "SELECT * FROM Invoices";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to search all
        /// </summary>
        public static string searchDistinct()
        {
            try
            {
                return "SELECT DISTINCT TotalCost FROM Invoices ORDER BY TotalCost";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by date range
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        public static string searchDate(string dateStart, string dateEnd)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by invoice number
        /// </summary>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchNum(string invoiceNumber)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by cost
        /// </summary>
        /// <param name="cost">int variable for total invoice cost</param>
        public static string searchCost(string cost)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE TotalCost = " + cost;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by date and invoice number
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchDateNum(string dateStart, string dateEnd, string invoiceNumber)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by date and cost
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="cost">int variable for total cost</param>
        public static string searchDateCost(string dateStart, string dateEnd, string cost)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE TotalCost = " + cost + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by cost and invoice number
        /// </summary>
        /// <param name="cost">int variable for total cost</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchCostNum(string cost, string invoiceNumber)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND TotalCost = " + cost;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set query to filter by date, invoice number, and cost.
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="cost">int variable for total cost</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchAll(string dateStart, string dateEnd, string cost, string invoiceNumber)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "# AND TotalCost = " + cost;
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