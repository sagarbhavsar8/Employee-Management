using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddToMyWebCart.Common
{
    public class Common
    {
        public static string getMonthName(int? monthCount)
        {
            string[] MonthName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            if (monthCount == null)
            {
                return MonthName[DateTime.Now.Month - 1];
            }
            else
            {
                return MonthName[Convert.ToInt32(monthCount) - 1];
            }
        }
    }
}