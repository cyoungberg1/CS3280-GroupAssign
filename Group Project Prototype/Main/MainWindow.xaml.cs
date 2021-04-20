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
        clsMainLogic logic = new clsMainLogic();

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

            logic.clearInvoice();
            workingInvoiceDataGrid.Items.Refresh();
            workingInvoiceDataGrid.ItemsSource = logic.GetInvoiceList();

            //itemsComboBox.ItemsSource = 

            BindingList<Items.clsItem> itemsList = logic.GetItems();
            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsComboBox.Items.Add(itemsList[i]);
            }
        }

        private void itemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items.clsItem Item;
            Item = (Items.clsItem)itemsComboBox.Items[itemsComboBox.SelectedIndex];

            itemCostTxtBox.Text = "$" + Item.Cost;
        }

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

        private void clearInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            logic.clearInvoice();
            workingInvoiceDataGrid.Items.Refresh();
            workingInvoiceDataGrid.ItemsSource = logic.GetInvoiceList();
        }

        private void saveInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Items.clsItem> items = logic.GetInvoiceList();
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
        }

        private void editInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logic.selectedInvoice == 0)
            {
                MessageBox.Show("You must search for an invoice first.");
            }
        }

        private void deleteInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (logic.selectedInvoice == 0)
            {
                MessageBox.Show("You must search for an invoice first.");
            }
        }
    }
}
