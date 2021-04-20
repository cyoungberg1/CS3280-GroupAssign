using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;


namespace Group_Project_Prototype.Main
{
    /// <summary>
    /// Class containing database access and SQL statements.
    /// </summary>
    class clsMainSQL
    {
        /// <summary>
        /// SQL used to update an invoice.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="invoiceNum"></param>
        /// <returns>SQL string</returns>
        public string UpdateInvoice(int cost, int invoiceNum)
        {
            string sql = "UPDATE Invoices SET TotalCost =" + cost + "WHERE InvoiceNum =" + invoiceNum;
            return sql;
        }
        /// <summary>
        /// SQL used to delete a LineItem entry.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns>SQL string</returns>
        public string DeleteLineItem(int invoiceNum, int lineItemNum, char itemCode)
        {
            string sql = "DELETE FROM LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineItemNum + "," + itemCode;
            return sql;
        }
        /// <summary>
        /// SQL used to delete an invoice.
        /// </summary>
        /// <param name="invoiceNum">The invoice number to be deleted.</param>
        /// <returns>SQL string</returns>
        public string DeleteInvoice(int invoiceNum)
        {
            string sql = "DELETE FROM Invoices WHERE InvoiceNum =" + invoiceNum;
            return sql;
        }
        /// <summary>
        /// SQL used to insert a line item.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns>SQL string</returns>
        public string InsertLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            string sql = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineItemNum + ", " + "'" + itemCode + "')";
            return sql;
        }
        /// <summary>
        /// SQL used to insert a new invoice.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns>SQL string</returns>
        public string InsertInvoice(string date, string cost)
        {
            string sql = "INSERT into Invoices (InvoiceDate, TotalCost) VALUES (#" + date + "#, " + cost + ")";
            return sql;
        }
        /// <summary>
        /// SQL used to select an invoice by its number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>SQL string</returns>
        public string SelectInvoiceByNumber(int invoiceNum)
        {
            string sql = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum =" + invoiceNum;
            return sql;
        }
        /// <summary>
        /// SQL to select all items.
        /// </summary>
        /// <returns>SQL string</returns>
        public string SelectItems()
        {
            string sql = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            return sql;
        }
        /// <summary>
        /// SQL used to select a Line Item based on an invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>SQL string</returns>
        public string SelectLineItem(int invoiceNum)
        {
            string sql = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum =" + invoiceNum;
            return sql;
        }

    }
}
