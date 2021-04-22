using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group_Project_Prototype.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// The purpose of this window and class is to
    /// search for an invoice using filters to interact
    /// with the database, select an invoice,
    /// and send the invoice number back to the main window.
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchLogic logic = new clsSearchLogic();
        clsGetInvoiceNumber number = new clsGetInvoiceNumber();
        clsGetInvoiceCost cost = new clsGetInvoiceCost();

        /// <summary>
        /// initialize the search window
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            loadInvoices();
            number.getInvoiceNumber();
            cost.getInvoiceCost();
            foreach (clsGetInvoiceNumber.Invoice item in number.numberList)
            {
                cboNumber.Items.Add(item);
            }
            foreach (clsGetInvoiceCost.Invoice item in cost.costList)
            {
                cboCharge.Items.Add(item);
            }
        }

        private void loadInvoices()
        {
            logic.getInvoiceData();
            dgrdInvoices.ItemsSource = null;
            dgrdInvoices.ItemsSource = logic.invoiceList;
        }

        /// <summary>
        /// method to handle the select button click event
        /// this will give the selected invoice number to MainWindow by
        /// saving the invoice ID to a local variable that the main window can access.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //close this window
                clsSearchLogic.Invoice selectedInvoice = (clsSearchLogic.Invoice)dgrdInvoices.SelectedItem;
                //wndMain.asdf = selectedInvoice.number;
                this.Close();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle the cancel button click event
        /// will close the search window and reopen the main window without sening any info
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //close this window
                this.Close();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //close this window
                this.Close();
                //handle the cancel ourselves
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle the clearfilters button click event
        /// will clear all the filters and the data grid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dateStart.SelectedDate = null;
                dateEnd.SelectedDate = null;
                cboCharge.SelectedItem = null;
                cboCharge.Text = "Select Total Charge Amount";
                cboNumber.SelectedItem = null;
                cboNumber.Text = "Select Invoice Number";
                logic.getInvoiceData();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle when the "invoice number" selection has changed
        /// will update the SQL query and the datagrid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void cboCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboCharge.SelectedItem == null)
                {
                    logic.costSelected = false;
                }
                else
                {
                    clsGetInvoiceCost.Invoice selectedInvoice = (clsGetInvoiceCost.Invoice)cboCharge.SelectedItem;
                    logic.cost = selectedInvoice.amount;
                    logic.costSelected = true;
                }

                search();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle when the "charge amount" selection has changed
        /// will update the SQL query and the datagrid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void cboNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboNumber.SelectedItem == null)
                {
                    logic.invoiceNumberSelected = false;
                }
                else
                {
                    clsGetInvoiceNumber.Invoice selectedInvoice = (clsGetInvoiceNumber.Invoice)cboNumber.SelectedItem;
                    logic.invoiceNumber = selectedInvoice.number;
                    logic.invoiceNumberSelected = true;
                }

                search();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle when the "start date" selection has changed
        /// will update the SQL query and the datagrid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dateStart.SelectedDate == null)
                {
                    logic.dateStartSelected = false;
                }
                else
                {
                    logic.dateStart = dateStart.SelectedDate.Value.ToString();
                    logic.dateStartSelected = true;
                }

                search();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// method to handle when the "end date" selection has changed
        /// will update the SQL query and the datagrid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void dateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dateEnd.SelectedDate == null)
                {
                    logic.dateEndSelected = false;
                }
                else
                {
                    logic.dateEnd = dateEnd.SelectedDate.Value.ToString();
                    logic.dateEndSelected = true;
                }

                search();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void search()
        {
            if ((logic.dateStartSelected == true && logic.dateEndSelected == false) || (logic.dateStartSelected == false && logic.dateEndSelected == true))
            {
                lblStart.FontWeight = FontWeights.Bold;
                lblStart.Foreground = Brushes.Red;
                lblEnd.FontWeight = FontWeights.Bold;
                lblEnd.Foreground = Brushes.Red;
            }
            else
            {
                lblStart.FontWeight = FontWeights.Normal;
                lblStart.Foreground = Brushes.Black;
                lblEnd.FontWeight = FontWeights.Normal;
                lblEnd.Foreground = Brushes.Black;
                loadInvoices();
            }
        }

        /// <summary>
        /// method to handle the error thown in the try catch blocks.
        /// </summary>
        /// <param name="sClass">the class name</param>
        /// <param name="sMethod">the method name</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
