using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItemsSQL
    {
        public int InAInvoice(string code)
        {
            int iRet = 0;
            string sSQL = "SELECT * FROM ItemDesc WHERE ItemCode = " + code;
            DataSet ds = new DataSet();
            //ds = ExecuteSQL(sSQL, ref iRet)

            return iRet;
        }

        public void UpdateItem(string code, string cost, string desc)
        {
            int iRet = 0;
            string sSQL = "UPDATE ItemDesc SET Cost = '" + cost + "', ItemDesc = '" + desc + "' " +
                "WHERE ItemCode = '" + code + "'";
            DataSet ds = new DataSet();
            //ds = ExecuteSQL(sSQL, ref iRet)
        }

        public DataSet GetItems()
        {
            int iRet = 0;
            string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            DataSet ds = new DataSet();
            //ds = ExecuteSQL(sSQL, ref iRet);
            return ds;
        }
    }
}
