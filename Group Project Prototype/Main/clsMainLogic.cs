using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Group_Project_Prototype.Main
{
    /// <summary>
    /// Class for the main window's primary logic.
    /// </summary>
    class clsMainLogic
    {
        //search window is going to pass back the invoice number that needs to be either edited or deleted 
        //method to update, select and delete
    }

    // selected invoice from the search screen
    int selectedInvoice;

    /// <summary>
    /// Class to hold invoice attributes.
    /// </summary>
    class Invoice
    {
        /// <summary>
        /// The item code for an item on the invoice.
        /// </summary>
        private char cItemCode;
        /// <summary>
        /// The item description for an item on the description 
        /// </summary>
        private string sItemDesc;
        private double dCost;

        public char ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public double Cost { get; set; }
    }
}
