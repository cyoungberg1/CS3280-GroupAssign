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
        /// <summary>
        /// objects for classes
        /// </summary>
        clsSearchLogic logic = new clsSearchLogic();
        clsGetInvoiceNumber number = new clsGetInvoiceNumber();
        clsGetInvoiceCost cost = new clsGetInvoiceCost();

        /// <summary>
        /// initialize the search window
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();

                //call loadInvoices() to fill the grid
                loadInvoices();

                //call getInvoiceNumber from the getInvoiceNumber class
                number.getInvoiceNumber();

                //call getInvoiceCost from the getInvoiceCost class
                cost.getInvoiceCost();

                //fill the comboboxes
                foreach (clsGetInvoiceNumber.Invoice item in number.numberList)
                {
                    cboNumber.Items.Add(item);
                }
                foreach (clsGetInvoiceCost.Invoice item in cost.costList)
                {
                    cboCharge.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// method to call the getInvoiceData() method from the logic class
        /// and update the data grid
        /// </summary>
        private void loadInvoices()
        {
            try
            {
                logic.getInvoiceData();
                dgrdInvoices.ItemsSource = null;
                dgrdInvoices.ItemsSource = logic.invoiceList;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
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
                if(dgrdInvoices.SelectedItem != null)
                {
                    //set the invoiceSelected bool to true in the main window
                    Main.MainWindow.invoiceSelected = true;

                    //create an Invoice object out of the selected row in the dataGrid
                    clsSearchLogic.Invoice selectedInvoice = (clsSearchLogic.Invoice)dgrdInvoices.SelectedItem;

                    //convert the invoice number property into an integer
                    int invoiceNum;
                    Int32.TryParse(selectedInvoice.number, out invoiceNum);

                    //set the selectedInvoiceNum in Main Window to the number property of the selected row
                    Main.MainWindow.selectedInvoiceNum = invoiceNum;

                    //close this window
                    this.Close();
                }
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
                //update invoiceSelected bool in main window to false
                Main.MainWindow.invoiceSelected = false;

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

        /// <summary>
        /// method to handle when the window is closed with the x button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //update invoiceSelected bool in main window to false
                Main.MainWindow.invoiceSelected = false;

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
                //set all the filter values to null and reset the combobox text
                dateStart.SelectedDate = null;
                dateEnd.SelectedDate = null;
                cboCharge.SelectedItem = null;
                cboCharge.Text = "Select Total Charge Amount";
                cboNumber.SelectedItem = null;
                cboNumber.Text = "Select Invoice Number";
                //call getInvoiceData to update the datagrid
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
                //check to see if nothing is selected in the charge combobox
                if (cboCharge.SelectedItem == null)
                {
                    //if that is the case, set the costSelected bool to false in the logic class
                    logic.costSelected = false;
                }
                else
                {
                    //if there is something selected, create an invoice object 
                    //and update the variables in the logic class to show it is selected and what it is
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

        /// <summary>
        /// method to check to make sure there is a start and end date before calling loadInvoices()
        /// </summary>
        private void search()
        {
            try
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
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
