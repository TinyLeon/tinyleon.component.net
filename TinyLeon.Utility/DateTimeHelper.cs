using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class DateTimeHelper
    {
        private static DateTime _MinDateTime = new DateTime(1970, 1, 1, 8, 0, 0);

        /// <summary>
        /// 将一个 DateTime 值转换为 int 类型。
        /// </summary>
        public static long DateTimeToInt(DateTime time)
        {
            //long diff = time.Ticks - new DateTime(2010, 1, 1).Ticks;

            //return (int)(diff / DateTimeToIntCarry);

            //return ((((long)((((time.Year - MiniYear) * 13 + time.Month) * 32 + time.Day) * 24)
            //    + time.Hour) * 60 + time.Minute) * 60 + time.Second) * 1000 + time.Millisecond;

            //return time.Ticks;
            return (long)(time - _MinDateTime).TotalSeconds;
        }

        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime GMT2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }
        /// <summary>
        /// 长整型日期转换为正常日期
        /// </summary>
        /// <param name="time">长整型时间</param>
        /// <returns></returns>
        public static DateTime LongToDateTime(long time)
        {
            return _MinDateTime.AddSeconds(time);
        }
    }
}
