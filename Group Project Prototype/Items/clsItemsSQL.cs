using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItemsSQL
    {
        /// <summary>
        /// returns the SQL to find what invoice a item is in, or if it is in an invoice 
        /// </summary>
        /// <param name="code">the code of the item that is being checked</param>
        /// <returns>the SQL string</returns>
        public string InAInvoice(string code)
        {
            try
            {
                return "select distinct(InvoiceNum) from LineItems where ItemCode = '" + code + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns the SQL to update the itemDesc table 
        /// </summary>
        /// <param name="code">the item code</param>
        /// <param name="cost">the item cost</param>
        /// <param name="desc">the item description</param>
        /// <returns>the SQL</returns>
        public string UpdateItem(string code, string cost, string desc)
        {
            try
            {
                return "UPDATE ItemDesc SET Cost = " + cost + ", ItemDesc = '" + desc + "' WHERE ItemCode = '" + code + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns the SQL to update the itemDesc table 
        /// </summary>
        /// <param name="code">the item code</param>
        /// <param name="desc">the item description</param>
        /// <returns>the SQL</returns>
        public string UpdateItemD(string code, string desc)
        {
            try
            {
                return "UPDATE ItemDesc SET ItemDesc = '" + desc + "' WHERE ItemCode = " + code + "";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns the SQL to update the itemDesc table 
        /// </summary>
        /// <param name="code">the item code</param>
        /// <param name="cost">the item cost</param>
        /// <returns>the SQL</returns>
        public string UpdateItemC(string code, string cost)
        {
            try
            {
                return "UPDATE ItemDesc SET Cost = " + cost + " WHERE ItemCode = '" + code + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// returns the sql to get all of the items from the database
        /// </summary>
        /// <returns>the SQL</returns>
        public string GetItems()
        {
            try
            {
                return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc ORDER BY ItemCode";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// returns the SQL to add an item to the database 
        /// </summary>
        /// <param name="code">the item code</param>
        /// <param name="cost">the item cost</param>
        /// <param name="desc">the item description</param>
        /// <returns>the SQL</returns>
        public string AddItem(string code, string cost, string desc)
        {
            try
            {
                return "Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + code + "', '" + desc + "', " + cost + ")";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// returns the SQl to delete an item from the databse 
        /// </summary>
        /// <param name="code">the item code</param>
        /// <returns>the SQL</returns>
        public string DeleteItem(string code)
        {
            try
            {
                return "Delete from ItemDesc Where ItemCode = '" + code + "'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}