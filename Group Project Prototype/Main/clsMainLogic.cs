using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace Group_Project_Prototype.Main
{
    /// <summary>
    /// Class for the main window's primary logic.
    /// </summary>
    class clsMainLogic
    {
        /// <summary>
        /// An object of the clsMainSQL class
        /// </summary>
        clsMainSQL sql = new clsMainSQL();

        /// <summary>
        /// An object of the DataSet class
        /// </summary>
        DataSet ds = new DataSet();

        /// <summary>
        /// An object of the clsDataAccess class
        /// </summary>
        clsDataAccess data = new clsDataAccess();

        /// <summary>
        /// List of clsItem objects for an invoice.
        /// </summary>
        ObservableCollection<Items.clsItem> WorkingInvoiceItems = new ObservableCollection<Items.clsItem>();

        /// <summary>
        /// The currently selected invoice to be viewed edited or deleted
        /// </summary>
        int iSelectedInvoice = 0;

        /// <summary>
        /// Full list of available items.
        /// </summary>
        ObservableCollection<Items.clsItem> itemsList;

        ObservableCollection<Items.clsItem> dgItems;

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>A list of item objects.</returns>
        public ObservableCollection<Items.clsItem> GetItems()
        {
            int retVal = 0;
            string itemsSQL = sql.SelectItems();
            ds = data.ExecuteSQLStatement(itemsSQL, ref retVal);

            itemsList = new ObservableCollection<Items.clsItem>();
            Items.clsItem item;

            for (int i = 0; i < retVal; i++)
            {
                item = new Items.clsItem(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][1].ToString());
                itemsList.Add(item);
            }

            return itemsList;
        }

        /// <summary>
        /// Adds an item to the current working invoice.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Items.clsItem item)
        {
            WorkingInvoiceItems.Add(item);
        }

        /// <summary>
        /// Gets a list of items on the current working invoice.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Items.clsItem> GetInvoiceList()
        {
            return WorkingInvoiceItems;
        }

        /// <summary>
        /// Gets the current working invoices cost.
        /// </summary>
        /// <returns>Total cost of the invoice.</returns>
        public string GetInvoiceCost()
        {
            int cost = 0;
            for (int i = 0; i < WorkingInvoiceItems.Count; i++)
            {
                cost += Int32.Parse(WorkingInvoiceItems[i].Cost);
            }
            return cost.ToString();
        }

        public int GetSelectedInvoiceCost()
        {
            int cost = 0;
            for (int i = 0; i < dgItems.Count; i++)
            {
                cost += Int32.Parse(dgItems[i].Cost);
            }
            return cost;
        }

        /// <summary>
        /// Clears the current working invoice.
        /// </summary>
        public void clearInvoice()
        {
            WorkingInvoiceItems.Clear();
        }

        /// <summary>
        /// Saves a working invoice to the Invoices database
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        public void SaveInvoice(string invoiceDate, string totalCost)
        {
            string insertInvoiceSQL = sql.InsertInvoice(invoiceDate, totalCost);

            data.ExecuteNonQuery(insertInvoiceSQL);

            string invoiceNumber = data.ExecuteScalarSQL("SELECT MAX(InvoiceNum) FROM Invoices");
            selectedInvoice = Int32.Parse(invoiceNumber);

            string insertLineItemSQL;

            dgItems = new ObservableCollection<Items.clsItem>();

            // insert a line item for each item on the invoice
            for (int i = 0; i < WorkingInvoiceItems.Count; i++)
            {
                dgItems.Add(WorkingInvoiceItems[i]);

                insertLineItemSQL = sql.InsertLineItem(Int32.Parse(invoiceNumber), (i+1), WorkingInvoiceItems[i].Code);
                data.ExecuteNonQuery(insertLineItemSQL);
            }

            
        }

        /// <summary>
        /// Displays the currently selected invoice in the selected invoice datagrid.
        /// </summary>
        /// <returns>An obvservable collection of invoices.</returns>
        public ObservableCollection<Items.clsItem> DisplayInvoie()
        {
            string getInvoiceSQL = sql.SelectLineItem(selectedInvoice);

            int retVal = 0;
            ds = data.ExecuteSQLStatement(getInvoiceSQL, ref retVal);

            dgItems = new ObservableCollection<Items.clsItem>();
            Items.clsItem item;

            for (int i = 0; i < retVal; i++)
            {
                item = new Items.clsItem(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][1].ToString());
                dgItems.Add(item);
            }

            return dgItems;
        }

        /// <summary>
        /// Updates the dgItems collection.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public void UpdateDataGridItem(Items.clsItem item, int index)
        {
            dgItems[index] = item;
        }

        /// <summary>
        /// Updates an invoice.
        /// </summary>
        /// <param name="invoiceNum">The invoice number.</param>
        /// <param name="lineItemNum">The line item number.</param>
        /// <param name="itemCode">The item code.</param>
        public void UpdateInvoice(int invoiceNum, int lineItemNum, string itemCode)
        {
            string updateLineItemSQL = sql.UpdateLineItem(invoiceNum, lineItemNum, itemCode);
            data.ExecuteNonQuery(updateLineItemSQL);

            string updateInvoiceSQL = sql.UpdateInvoice(GetSelectedInvoiceCost(), invoiceNum);
            data.ExecuteNonQuery(updateInvoiceSQL);
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="invoiceNum">The invoice number.</param>
        public void DeleteInvoice(int invoiceNum)
        {
            string deleteLineItemSQL = sql.DeleteLineItem(invoiceNum);
            data.ExecuteNonQuery(deleteLineItemSQL);

            string deleteInvoiceSQL = sql.DeleteInvoice(invoiceNum);
            data.ExecuteNonQuery(deleteInvoiceSQL);

            dgItems.Clear();
        }

        /// <summary>
        /// Removes an item from the working invoice list.
        /// </summary>
        /// <param name="index"></param>
        public void DeleteWorkingInvoiceItem(int index)
        {
            WorkingInvoiceItems.RemoveAt(index);
        }


        /// <summary>
        /// Getter and setter for the selectedInvoice field.
        /// </summary>
        public int selectedInvoice
        {
            get { return iSelectedInvoice; }
            set { 
                iSelectedInvoice = value; 
            }
        }

    }

    /// <summary>
    /// Class to hold invoice attributes.
    /// </summary>
    //class Invoice
    //{
    //    /// <summary>
    //    /// The item code for an item on the invoice.
    //    /// </summary>
    //    private char cItemCode;
    //    /// <summary>
    //    /// The item description for an item on the description 
    //    /// </summary>
    //    private string sItemDesc;
    //    private double dCost;

    //    public char ItemCode { get; set; }
    //    public string ItemDesc { get; set; }
    //    public double Cost { get; set; }
    //}
}
