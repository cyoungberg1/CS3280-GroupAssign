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
            try
            {
                return "UPDATE Invoices SET TotalCost = " + cost + " WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to update line items for an invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string UpdateLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                return "UPDATE LineItems SET ItemCode = '" + itemCode + "' WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum = " + lineItemNum;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to delete a LineItem entry.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns>SQL string</returns>
        public string DeleteLineItem(int invoiceNum)
        {
            try
            {
                return "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to delete an invoice.
        /// </summary>
        /// <param name="invoiceNum">The invoice number to be deleted.</param>
        /// <returns>SQL string</returns>
        public string DeleteInvoice(int invoiceNum)
        {
            try
            {
                return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
            try
            {
                return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + "," + lineItemNum + ", " + "'" + itemCode + "')";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to insert a new invoice.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns>SQL string</returns>
        public string InsertInvoice(string date, string cost)
        {
            try
            {
                return "INSERT into Invoices (InvoiceDate, TotalCost) VALUES (#" + date + "#, " + cost + ")";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to select an invoice by its number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>SQL string</returns>
        public string SelectInvoiceByNumber(int invoiceNum)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum =" + invoiceNum;

            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL to select all items.
        /// </summary>
        /// <returns>SQL string</returns>
        public string SelectItems()
        {
            try
            {
                return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL used to select a Line Item based on an invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>SQL string</returns>
        public string SelectLineItem(int invoiceNum)
        {
            try
            {
                return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum =" + invoiceNum;
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
