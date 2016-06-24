using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TinyLeon.Utility
{
    public static class EnumHelper
    {
        public static List<SelectListItem> GetSelectItemList(this Enum value, object selectedValue = null, string emptyChoice = "")
        {
            Type etype = value.GetType();
            if (!etype.Name.ToLower().Contains("enum"))
            {
                return null;
            }
            var enums = Enum.GetValues(etype);
            List<SelectListItem> resultList = new List<SelectListItem>();
            foreach (Enum item in enums)
            {
                resultList.Add(new SelectListItem
                {
                    Text = item.GetEnumDes(),
                    Value = item.GetHashCode().ToString(),
                    Selected = selectedValue == null ? false : selectedValue.ToString() == item.GetHashCode().ToString()
                });
            }
            if (!string.IsNullOrEmpty(emptyChoice))
            {
                resultList.Add(new SelectListItem
                {
                    Text = emptyChoice,
                    Value = "0"
                });
            }
            return resultList;
        }

        public static string GetEnumDes(this Enum value)
        {
            if (value == null) return "";
            FieldInfo field = value.GetType().GetField(value.ToString());
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))).Description;
        }

        /// <summary>
        /// 根据特定的枚举值名称获得枚举值的Description特性的值
        /// </summary>
        /// <param name="type">要查找特性的枚举值</param>
        /// <param name="name">字段名称</param>
        /// <returns>返回查找到的Description特性的值，如果没有，就返回.ToString()</returns>
        public static string GetEnumDes(Type type, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                FieldInfo fi = type.GetField(name);
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : name;
            }
            return string.Empty;
        }

        public static T GetEnumByValue<T>(int value) where T : struct,IConvertible
        {
            try
            {
                if (typeof(T).IsEnum)
                {
                    return Enum.IsDefined(typeof(T), value) ? (T)Enum.ToObject(typeof(T), value) : default(T);
                }
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        /// <summary>
        /// 根据值获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>``0.</returns>
        public static T GetEnumByName<T>(string name) where T : struct, IConvertible
        {
            try
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), name);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
            return default(T);
        }

        
    }
}
