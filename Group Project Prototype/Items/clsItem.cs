using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Prototype.Items
{
    public class clsItem
    {
        #region Attributes
        /// <summary>
        /// the code for the item
        /// </summary>
        private string sCode;
        /// <summary>
        /// the items cost in dollars 
        /// </summary>
        private string sCost;
        /// <summary>
        /// the items description
        /// </summary>
        private string sDesc;
        #endregion
        #region Constructors 
        /// <summary>
        /// overloaded constructor 
        /// </summary>
        /// <param name="code">the itmes code</param>
        /// <param name="cost">the items cost</param>
        /// <param name="desc">the items deciption</param>
        public clsItem(string code, string cost, string desc)
        {
            sCode = code;
            sCost = cost;
            sDesc = desc;
        }
        /// <summary>
        /// default constructor 
        /// </summary>
        public clsItem(){}
        #endregion
        #region Getters and Setters
        /// <summary>
        /// Getter and setter for code
        /// </summary>
        public string Code
        {
            get { return sCode; }
            set { sCode = value; }
        }
        /// <summary>
        /// getter and setter for cost
        /// </summary>
        public string Cost
        {
            get { return sCost; }
            set { sCost = value; }
        }
        /// <summary>
        /// getter and setter for description
        /// </summary>
        public string Desc
        {
            get { return sDesc; }
            set { sDesc = value; }
        }
        #endregion
        #region Methods 
        /// <summary>
        /// overrides the ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return sCode + " | "  + sDesc + ", $" + sCost;
        }
        #endregion
    }
}
