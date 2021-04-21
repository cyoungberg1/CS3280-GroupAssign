using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItemsLogic
    {
        private clsItemsSQL SQLCalls;
        private clsItem item;
        private clsDataAccess data;

        public clsItemsLogic()
        {
            SQLCalls = new clsItemsSQL();
            data = new clsDataAccess();
        }

        public void DeleteItem(string code)
        {
            //if not SQLCalls.InAInvoice(code)
            // - returns iRet
            // if iRet = 0
            // - delete
            // else 
            // - warn not able to delete 
        }

        public void EditItem(string code, string cost, string desc)
        {
            SQLCalls.UpdateItem(code, cost, desc);
        }

        public ObservableCollection<clsItem> GetItems()
        {
            DataSet ds = new DataSet();
            ObservableCollection<clsItem> Items = new ObservableCollection<clsItem>();
            ds = SQLCalls.GetItems();

            //loop through ds 
            //create new item
            //add to item to Items
            return Items;
        }

        public string getCode(string text)
        {
            return text.Substring(0, 1);
        }
    }
}
