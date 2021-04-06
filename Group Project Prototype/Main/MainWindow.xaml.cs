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

namespace Group_Project_Prototype.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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
            //itemsComboBox.ItemsSource = 
        }
    }
}
