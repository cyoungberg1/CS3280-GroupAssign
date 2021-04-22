using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

namespace Group_Project_Prototype.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// the referane to the logic
        /// </summary>
        clsItemsLogic itemsLogic;
        /// <summary>
        /// the selected items code
        /// </summary>
        string sSelectedCode;
        /// <summary>
        /// costructor 
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();
                itemsLogic = new clsItemsLogic(this);

                cbItems.ItemsSource = itemsLogic.GetItems();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// runs when the return to main menu button is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
        /// deletes the seleceted item from the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemsLogic.DeleteItem(sSelectedCode))
                    itemsLogic.GetItems();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// edits the selected item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sCost = tbCost.Text.ToString();
                string sDedc = tbDesc.Text.ToString();

                double dCost;
                if (Double.TryParse(sCost, out dCost))
                {
                    if (String.IsNullOrEmpty(sCost) && String.IsNullOrEmpty(sDedc))
                    {
                        ShowError("Please input a cost or discirption to edit");
                    }
                    else
                    {
                        tbCost.Text = "";
                        tbDesc.Text = "";
                        HideError();
                        itemsLogic.EditItem(sSelectedCode, sCost, sDedc);
                        itemsLogic.GetItems();
                    }
                }
                else
                {
                    ShowError("Please input a valid number into cost");
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
        /// changes what item is selected 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbItems.SelectedItem != null)
                {
                    string sItem = cbItems.SelectedItem.ToString();
                    string sCode = itemsLogic.getCode(sItem);
                    sSelectedCode = sCode;
                    HideError();
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
        /// shows an error message on the UI
        /// </summary>
        /// <param name="error">the error message</param>
        public void ShowError(string error)
        {
            try
            {
                lblError.Content = error;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// hides any error that is being diaplyed 
        /// </summary>
        public void HideError()
        {
            try
            {
                lblError.Content = "";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// changes how the window closes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// hanles errors 
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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
        /// <summary>
        /// adds new items to the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sCost = tbCost.Text.ToString();
                string sDesc = tbDesc.Text.ToString();


                double dCost;
                if (Double.TryParse(sCost, out dCost))
                {

                    if (String.IsNullOrEmpty(sCost))
                    {
                        ShowError("Please fill in the cost for the item");
                    }
                    else if (String.IsNullOrEmpty(sDesc))
                    {
                        ShowError("Please fill in the description for the item");
                    }
                    else
                    {
                        tbCost.Text = "";
                        tbDesc.Text = "";
                        HideError();
                        itemsLogic.AddItem(sDesc, sCost);
                        itemsLogic.GetItems();
                    }
                }
                else
                {
                    ShowError("Please input a valid number into cost");
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}