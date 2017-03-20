using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class RegxHelper
    {
        public static string CoverMobile(string mobile)
        {
            string mobileReg = @"^1\d{10}";
            Regex mReg = new Regex(mobileReg);
            if (!string.IsNullOrEmpty(mobile) && mReg.IsMatch(mobile))
            {
                return mobile.Substring(0, 7) + "****";
            }
            return mobile;
        }
    }
}
