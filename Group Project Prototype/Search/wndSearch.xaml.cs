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

//THIS IS A TEST COMMENT
//Here is another test comment

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
        /// initialize the search window
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// this is where the selected invoice number will be stored so that the main window can access it.
        /// </summary>
        int invoiceNumber;

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
        private void cboNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

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
        private void cboCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
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
