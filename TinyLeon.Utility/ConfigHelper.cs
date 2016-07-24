using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class ConfigHelper
    {
        /// <summary>
        /// 取枚举类型字符串
        /// </summary>
        /// <param name="key">对应的客户端版本类型做为key</param>
        /// <returns>System.String.</returns>
        public static string GetConfig(string key)
        {
            string appConfig = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(appConfig))
            {
                return appConfig;
            }
            return string.Empty;
        }
    }
}
