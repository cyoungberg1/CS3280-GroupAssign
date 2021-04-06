using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItem
    {
        /// <summary>
        /// the code for the item
        /// </summary>
        private string sCode;
        /// <summary>
        /// the items cost in dollars 
        /// </summary>
        private string sCost;
        private string sDesc;

        public clsItem(string code, string cost, string desc)
        {
            sCode = code;
            sCost = cost;
            sDesc = desc;
        }

        public string Code
        {
            get { return sCode; }
            set { sCode = value; }
        }

        public string Cost
        {
            get { return sCost; }
            set { sCost = value; }
        }

        public string Desc
        {
            get { return sDesc; }
            set { sDesc = value; }
        }

        public override string ToString()
        {
            return sCode + "| "  + sDesc + " - " + sCost;
        }
    }
}
