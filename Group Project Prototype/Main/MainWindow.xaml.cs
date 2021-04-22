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
        clsMainLogic logic = new clsMainLogic();

        /// <summary>
        /// The item currently selected in the selected invoice datagrid
        /// </summary>
        int selectedInvoiceSelectedItem;

        /// <summary>
        /// The item currently selected in the working invoice datagrid
        /// </summary>
        int workingInvoiceSelectedItem;

        public MainWindow()
        {
            InitializeComponent();
            addInvoiceCanvas.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Opens the search page or items page when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as MenuItem).Name == "searchMenuItem")
            {
                // displays the search window
                Search.wndSearch search = new Search.wndSearch();

                search.ShowDialog();

                // will be set by search window. just for testing right now
                logic.selectedInvoice = 5053;

                addInvoiceCanvas.Visibility = Visibility.Hidden;

                selectedInvoiceDataGrid.ItemsSource = logic.DisplayInvoie();

            }
            else if ((sender as MenuItem).Name == "editMenuItem")
            {
                // displays the items window
                Items.wndItems itms = new Items.wndItems();

                itms.ShowDialog();

                //requery the combo box in case of changes made to item list.
            }
        }

        /// <summary>
        /// Populates the add invoice section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            addInvoiceCanvas.Visibility = Visibility.Visible;
            invoiceAddedCanvas.Visibility = Visibility.Hidden;
            itemEditCanvas.Visibility = Visibility.Hidden;

            logic.clearInvoice();
            workingInvoiceDataGrid.Items.Refresh();
            workingInvoiceDataGrid.ItemsSource = logic.GetInvoiceList();

            ObservableCollection<Items.clsItem> itemsList = logic.GetItems();
            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsComboBox.Items.Add(itemsList[i]);
            }
        }

        /// <summary>
        /// Updates the cost textbox to represent the cost of the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items.clsItem Item;
            Item = (Items.clsItem)itemsComboBox.Items[itemsComboBox.SelectedIndex];

            itemCostTxtBox.Text = "$" + Item.Cost;
        }

        /// <summary>
        /// Adds the selected item to the current working invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Clears and resets the current working invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            logic.clearInvoice();
            totalCostTxtBox.Text = "$" + logic.GetInvoiceCost();
        }

        /// <summary>
        /// Saves the current working invoice to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInvoiceBtn_Click(object sender, RoutedEventArgs e)
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

            logic.SaveInvoice(invoiceDate.Text, logic.GetInvoiceCost());
            addInvoiceCanvas.Visibility = Visibility.Hidden;
            invoiceAddedCanvas.Visibility = Visibility.Visible;

            selectedInvoiceDataGrid.ItemsSource = logic.DisplayInvoie();
        }

        /// <summary>
        /// Displays the edit invoice form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logic.selectedInvoice == 0)
            {
                MessageBox.Show("You must search for an invoice first.");
                return;
            }

            itemEditCanvas.Visibility = Visibility.Visible;
            invoiceAddedCanvas.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Deletes the currently selected invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logic.selectedInvoice == 0)
            {
                MessageBox.Show("You must search for an invoice first.");
                return;
            }

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete this invoice?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                logic.DeleteInvoice(logic.selectedInvoice);
                itemEditCanvas.Visibility = Visibility.Hidden;
                logic.selectedInvoice = 0;
            }
        }

        /// <summary>
        /// Updates the edit item form with info from the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectedInvoiceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Items.clsItem item = (Items.clsItem)selectedInvoiceDataGrid.SelectedItem;

            if (item == null)
            {
                return;
            }

            selectedInvoiceSelectedItem = selectedInvoiceDataGrid.SelectedIndex;

            selectedItemCostTxt.Text = item.Cost;
            selectedItemDescTxt.Text = item.Desc;

            ObservableCollection<Items.clsItem> itemsList = logic.GetItems();

            for (int i = 0; i < itemsList.Count; i++)
            {
                updateItemCbo.Items.Add(itemsList[i]);
            }
        }

        /// <summary>
        /// Populates the updateItem combo box with items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateItemCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items.clsItem Item;
            Item = (Items.clsItem)updateItemCbo.Items[updateItemCbo.SelectedIndex];

            newItemCostTxt.Text = "$" + Item.Cost;
        }

        /// <summary>
        /// Saves the updates made to an invoice item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveUpdatesBtn_Click(object sender, RoutedEventArgs e)
        {
            Items.clsItem Item;
            Item = (Items.clsItem)updateItemCbo.Items[updateItemCbo.SelectedIndex];

            logic.UpdateDataGridItem(Item, selectedInvoiceSelectedItem);

            Items.clsItem item = (Items.clsItem)updateItemCbo.SelectedItem;

            logic.UpdateInvoice(logic.selectedInvoice, selectedInvoiceSelectedItem+1, item.Code);
        }

        /// <summary>
        /// Removes the selected item from the working invoice datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            logic.DeleteWorkingInvoiceItem(workingInvoiceSelectedItem);
            deleteItemBtn.Visibility = Visibility.Hidden;
            totalCostTxtBox.Text = "$" + logic.GetInvoiceCost();
        }

        /// <summary>
        /// Sets the working invoice selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workingInvoiceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Items.clsItem item = (Items.clsItem)workingInvoiceDataGrid.SelectedItem;

            if (item == null)
            {
                return;
            }

            // the index to be removed
            workingInvoiceSelectedItem = workingInvoiceDataGrid.SelectedIndex;
            deleteItemBtn.Visibility = Visibility.Visible;
        }
    }
}
