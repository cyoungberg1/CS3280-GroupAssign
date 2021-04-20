using System;
using System.Collections.Generic;
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
        List<Items.clsItem> InvoiceItems = new List<Items.clsItem>();

        /// <summary>
        /// The currently selected invoice to be viewed edited or deleted
        /// </summary>
        int iSelectedInvoice = 0;

        /// <summary>
        /// Full list of available items.
        /// </summary>
        BindingList<Items.clsItem> itemsList;

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns>A list of item objects.</returns>
        public BindingList<Items.clsItem> GetItems()
        {
            int retVal = 0;
            string itemsSQL = sql.SelectItems();
            ds = data.ExecuteSQLStatement(itemsSQL, ref retVal);

            //testing
            //end testing

            itemsList = new BindingList<Items.clsItem>();
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
            InvoiceItems.Add(item);
        }

        /// <summary>
        /// Gets a list of items on the current working invoice.
        /// </summary>
        /// <returns></returns>
        public List<Items.clsItem> GetInvoiceList()
        {
            return InvoiceItems;
        }

        /// <summary>
        /// Gets the current working invoices cost.
        /// </summary>
        /// <returns>Total cost of the invoice.</returns>
        public string GetInvoiceCost()
        {
            int cost = 0;
            for (int i = 0; i < InvoiceItems.Count; i++)
            {
                cost += Int32.Parse(InvoiceItems[i].Cost);
            }
            return cost.ToString();
        }

        /// <summary>
        /// Clears the current working invoice.
        /// </summary>
        public void clearInvoice()
        {
            InvoiceItems.Clear();
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

            string insertLineItemSQL;

            // insert a line item for each item on the invoice
            for (int i = 0; i < InvoiceItems.Count; i++)
            {
                insertLineItemSQL = sql.InsertLineItem(Int32.Parse(invoiceNumber), (i+1), InvoiceItems[i].Code);
                data.ExecuteNonQuery(insertLineItemSQL);
            }
        }

        /// <summary>
        /// Getter and setter for the selectedInvoice field.
        /// </summary>
        public int selectedInvoice
        {
            get { return iSelectedInvoice; }
            set { iSelectedInvoice = value; }
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
