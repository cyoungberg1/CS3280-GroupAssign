using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace Group_Project_Prototype.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// An object of the clsMainLogic class
        /// </summary>
        public clsMainLogic logic = new clsMainLogic();

        /// <summary>
        /// A list of items.
        /// </summary>
        ObservableCollection<Items.clsItem> itemsList;

        /// <summary>
        /// The item currently selected in the selected invoice datagrid
        /// </summary>
        int selectedInvoiceSelectedItem;

        /// <summary>
        /// The item currently selected in the working invoice datagrid
        /// </summary>
        int workingInvoiceSelectedItem;

        /// <summary>
        /// Constructor for the main window.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                addInvoiceCanvas.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Opens the search page or items page when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as MenuItem).Name == "searchMenuItem")
                {
                    // displays the search window
                    Search.wndSearch search = new Search.wndSearch(logic);

                    search.ShowDialog();

                    addInvoiceCanvas.Visibility = Visibility.Hidden;

                    // displays selected invoice in the datagrid
                    selectedInvoiceDataGrid.ItemsSource = logic.DisplayInvoie();

                    // update label dislays invoice number
                    selectedInvoiceLbl.Content = "Selected Invoice #" + logic.selectedInvoice;


                }
                else if ((sender as MenuItem).Name == "editMenuItem")
                {
                    // displays the items window
                    Items.wndItems itms = new Items.wndItems();

                    itms.ShowDialog();

                    addInvoiceCanvas.Visibility = Visibility.Hidden;

                    itemsComboBox.Text = "";
                    itemsComboBox.SelectedIndex = -1;
                    itemsComboBox.Items.Clear();

                    itemCostTxtBox.Text = "";
                    totalCostTxtBox.Text = "";
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
        /// Populates the add invoice section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // show controls
                addInvoiceCanvas.Visibility = Visibility.Visible;
                invoiceAddedCanvas.Visibility = Visibility.Hidden;
                itemEditCanvas.Visibility = Visibility.Hidden;

                // reset and refresh invoice
                logic.clearInvoice();
                workingInvoiceDataGrid.Items.Refresh();
                workingInvoiceDataGrid.ItemsSource = logic.GetInvoiceList();

                // reset controls
                itemCostTxtBox.Text = "";
                totalCostTxtBox.Text = "";
                itemsComboBox.SelectedIndex = -1;

                //fill items combo box
                itemsList = logic.GetItems();
                for (int i = 0; i < itemsList.Count; i++)
                {
                    itemsComboBox.Items.Add(itemsList[i]);
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
        /// Updates the cost textbox to represent the cost of the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (itemsComboBox.SelectedIndex < 0)
                {
                    return;
                }

                // cast selection to an clsItem object
                Items.clsItem Item;
                Item = (Items.clsItem)itemsComboBox.Items[itemsComboBox.SelectedIndex];

                // display cost of the item
                itemCostTxtBox.Text = "$" + Item.Cost;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Adds the selected item to the current working invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemsComboBox.Text == "")
                {
                    MessageBox.Show("Please select an item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // add selected item to the invoice list
                logic.AddItem((Items.clsItem)itemsComboBox.Items[itemsComboBox.SelectedIndex]);

                // update the total cost for the invoice

                totalCostTxtBox.Text = "$" + logic.GetInvoiceCost();
                // refresh the working invoice datagrid
                workingInvoiceDataGrid.Items.Refresh();

                // populate the working invoice datagrid
                workingInvoiceDataGrid.ItemsSource = logic.GetInvoiceList();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Clears and resets the current working invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logic.clearInvoice();
                totalCostTxtBox.Text = "$" + logic.GetInvoiceCost();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Saves the current working invoice to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Items.clsItem> items = logic.GetInvoiceList();
                if (items.Count < 1)
                {
                    MessageBox.Show("Please add at least one item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (invoiceDate.Text == "")
                {
                    MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // saves invoice to the database
                logic.SaveInvoice(invoiceDate.Text, logic.GetInvoiceCost());
                addInvoiceCanvas.Visibility = Visibility.Hidden;
                invoiceAddedCanvas.Visibility = Visibility.Visible;

                // displays created invoice to the selected invoice datagrid
                selectedInvoiceDataGrid.ItemsSource = logic.DisplayInvoie();

                // displays invoice number
                selectedInvoiceLbl.Content = "Selected Invoice #" + logic.selectedInvoice;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Displays the edit invoice form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (logic.selectedInvoice == 0)
                {
                    MessageBox.Show("You must search for an invoice first.");
                    return;
                }

                // displays the edit invoice screen
                itemEditCanvas.Visibility = Visibility.Visible;
                invoiceAddedCanvas.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Deletes the currently selected invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (logic.selectedInvoice == 0)
                {
                    MessageBox.Show("You must search for an invoice first.");
                    return;
                }

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete this invoice?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    // deletes the selected invoice from the database
                    logic.DeleteInvoice(logic.selectedInvoice);
                    itemEditCanvas.Visibility = Visibility.Hidden;
                    logic.selectedInvoice = 0;
                    selectedInvoiceLbl.Content = "Selected Invoice";
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
        /// Updates the edit item form with info from the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedInvoiceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                // casts selection to a clsItem object
                Items.clsItem item = (Items.clsItem)selectedInvoiceDataGrid.SelectedItem;

                if (item == null)
                {
                    return;
                }

                // gets the index of the selected item
                selectedInvoiceSelectedItem = selectedInvoiceDataGrid.SelectedIndex;

                selectedItemCostTxt.Text = item.Cost;
                selectedItemDescTxt.Text = item.Desc;

                ObservableCollection<Items.clsItem> itemsList = logic.GetItems();

                for (int i = 0; i < itemsList.Count; i++)
                {
                    updateItemCbo.Items.Add(itemsList[i]);
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
        /// Populates the updateItem combo box with items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateItemCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // casts selection to a clsItem object to get the cost
                Items.clsItem Item;
                Item = (Items.clsItem)updateItemCbo.Items[updateItemCbo.SelectedIndex];

                newItemCostTxt.Text = "$" + Item.Cost;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Saves the updates made to an invoice item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveUpdatesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Items.clsItem Item;
                Item = (Items.clsItem)updateItemCbo.Items[updateItemCbo.SelectedIndex];

                logic.UpdateDataGridItem(Item, selectedInvoiceSelectedItem);

                Items.clsItem item = (Items.clsItem)updateItemCbo.SelectedItem;

                logic.UpdateInvoice(logic.selectedInvoice, selectedInvoiceSelectedItem + 1, item.Code);
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Removes the selected item from the working invoice datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // deletes selected item from the working invoice
                logic.DeleteWorkingInvoiceItem(workingInvoiceSelectedItem);
                deleteItemBtn.Visibility = Visibility.Hidden;
                totalCostTxtBox.Text = "$" + logic.GetInvoiceCost();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Sets the working invoice selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workingInvoiceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                // casts selection to a clsItem object
                Items.clsItem item = (Items.clsItem)workingInvoiceDataGrid.SelectedItem;

                if (item == null)
                {
                    return;
                }

                // the index to be removed
                workingInvoiceSelectedItem = workingInvoiceDataGrid.SelectedIndex;
                deleteItemBtn.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Handles errors.
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
    }
}
