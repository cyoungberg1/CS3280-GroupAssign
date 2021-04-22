using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItemsLogic
    {
        /// <summary>
        /// reference to the SQL statments 
        /// </summary>
        private clsItemsSQL SQL;
        /// <summary>
        /// used to make the item list
        /// </summary>
        private clsItem item;
        /// <summary>
        /// used to run the sql 
        /// </summary>
        private clsDataAccess data;
        /// <summary>
        /// reference to the items window 
        /// </summary>
        private wndItems wnd;
        /// <summary>
        /// used in the insert method
        /// </summary>
        private string sNewCode;
        /// <summary>
        /// also used in the insert method 
        /// </summary>
        private char cInsertCode;
        /// <summary>
        /// used to display all of the items
        /// </summary>
        ObservableCollection<clsItem> Items;
        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="window">the refernec to the items window</param>
        public clsItemsLogic(wndItems window)
        {
            try
            {
                SQL = new clsItemsSQL();
                data = new clsDataAccess();
                wnd = window;
                sNewCode = "A";
                cInsertCode = 'A';
                Items = new ObservableCollection<clsItem>();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// deletes an item in the database if that item is not in an invoice 
        /// </summary>
        /// <param name="code">the items code</param>
        /// <returns>if something was deleted</returns>
        public bool DeleteItem(string code)
        {
            try
            {
                int iRet = 0;
                DataSet ds = new DataSet();
                try
                {
                    ds = data.ExecuteSQLStatement(SQL.InAInvoice(code), ref iRet);
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }

                if (iRet == 0)
                {
                    try
                    {
                        int del = data.ExecuteNonQuery(SQL.DeleteItem(code));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                    }
                    wnd.ShowError("Deleted");
                    return true;
                }
                else
                {
                    string sError = "This Item is in Invoice ";
                    bool bFirst = true;
                    for (int i = 0; i < iRet; i++)
                    {
                        if (!bFirst)
                            sError += ", ";

                        sError += ds.Tables[0].Rows[i][0].ToString();
                    }

                    wnd.ShowError(sError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// edits an item in the database
        /// </summary>
        /// <param name="code">the items code</param>
        /// <param name="cost">the items cost</param>
        /// <param name="desc">the items description</param>
        public void EditItem(string code, string cost, string desc)
        {
            try
            {
                int iRet = 0;

                if (String.IsNullOrEmpty(cost))
                {
                    try
                    {
                        iRet = data.ExecuteNonQuery(SQL.UpdateItemD(code, desc));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                    }
                }
                else if (String.IsNullOrEmpty(desc))
                {
                    try
                    {
                        iRet = data.ExecuteNonQuery(SQL.UpdateItemC(code, cost));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        iRet = data.ExecuteNonQuery(SQL.UpdateItem(code, cost, desc));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// gets all of the items from the database and puts them into an observablecollection
        /// </summary>
        /// <returns>the colection of items</returns>
        public ObservableCollection<clsItem> GetItems()
        {
            try
            {
                Items.Clear();
                int iRet = 0;
                DataSet ds = new DataSet();
                try
                {
                    ds = data.ExecuteSQLStatement(SQL.GetItems(), ref iRet);
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }

                for (int i = 0; i < iRet; i++)
                {
                    item = new clsItem();
                    item.Code = ds.Tables[0].Rows[i][0].ToString();
                    item.Desc = ds.Tables[0].Rows[i][1].ToString();
                    item.Cost = ds.Tables[0].Rows[i][2].ToString();

                    Items.Add(item);
                }
                return Items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// gets the code from the inputed display item
        /// </summary>
        /// <param name="text">the display item</param>
        /// <returns>the items code</returns>
        public string getCode(string text)
        {
            try
            {
                return text.Substring(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// adds a new item to the database 
        /// </summary>
        /// <param name="desc">the items description</param>
        /// <param name="cost">the items cost</param>
        public void AddItem(string desc, string cost)
        {
            try
            {
                int iRet = 0;
                string sCode = sNewCode + cInsertCode.ToString();
                iRet = data.ExecuteNonQuery(SQL.AddItem(sCode, cost, desc));
                cInsertCode++;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
