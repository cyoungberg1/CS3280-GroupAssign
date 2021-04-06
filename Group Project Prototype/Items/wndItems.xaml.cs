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
        clsItemsSQL SQLCalls;
        clsItemsLogic itemsLogic;
        string sSelectedCode;

        public wndItems()
        {
            InitializeComponent();
            SQLCalls = new clsItemsSQL();
            itemsLogic = new clsItemsLogic(SQLCalls);

            //call get items in logic - apply to cbItems.ItemsSource

        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //call delete in logic - sending sSelectedCode
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //validate tbCost 
            //validate tbDesc
            //call edit in logic - sending sSelectedCode, input from tbCost, input from tbDesc
        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //call logic get code from text - apply to sSelectedCode
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
