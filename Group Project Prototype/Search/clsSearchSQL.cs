using System;
using System.Collections.Generic;
using System.Linq;
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
            return "SELECT * FROM Invoices";
        }

        /// <summary>
        /// set query to filter by date range
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        public static string searchDate(DateTime dateStart, DateTime dateEnd)
        {
            return "SELECT * FROM Invoices WHERE InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
        }

        /// <summary>
        /// set query to filter by invoice number
        /// </summary>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchNum(int invoiceNumber)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber;
        }

        /// <summary>
        /// set query to filter by cost
        /// </summary>
        /// <param name="cost">int variable for total invoice cost</param>
        public static string searchCost(int cost)
        {
            return "SELECT * FROM Invoices WHERE TotalCost = " + cost;
        }

        /// <summary>
        /// set query to filter by date and invoice number
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchDateNum(DateTime dateStart, DateTime dateEnd, int invoiceNumber)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
        }

        /// <summary>
        /// set query to filter by date and cost
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="cost">int variable for total cost</param>
        public static string searchDateCost(DateTime dateStart, DateTime dateEnd, int cost)
        {
            return "SELECT * FROM Invoices WHERE TotalCost = " + cost + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "#";
        }

        /// <summary>
        /// set query to filter by cost and invoice number
        /// </summary>
        /// <param name="cost">int variable for total cost</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchCostNum(int cost, int invoiceNumber)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND TotalCost = " + cost;
        }

        /// <summary>
        /// set query to filter by date, invoice number, and cost.
        /// </summary>
        /// <param name="dateStart">date variable for start date</param>
        /// <param name="dateEnd">date variable for end date</param>
        /// <param name="cost">int variable for total cost</param>
        /// <param name="invoiceNumber">int variable for invoice number</param>
        public static string searchAll(DateTime dateStart, DateTime dateEnd, int cost, int invoiceNumber)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNumber + " AND InvoiceDate BETWEEN #" + dateStart + "# AND #" + dateEnd + "# AND TotalCost = " + cost;
        }
    }
}
