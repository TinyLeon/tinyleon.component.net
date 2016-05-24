//作者：Leon
//日期：2015-12-18
//功能：提供datatable与强类型对象的转换
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace TinyLeon.Utility
{
    public class EntityHelper
    {
        #region 集合转换DataSet(仅lis参数)
        /// <summary> 
        /// 集合转换DataSet(仅lis参数)
        /// </summary> 
        /// <param name="p_List">集合</param>
        /// <remarks>
        /// CreateBy:wangjun
        /// </remarks>
        /// <returns></returns>
        public static DataSet ToDataSet(IList p_List)
        {
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(p_List[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }

            result.Tables.Add(_DataTable);
            return result;
        }
        #endregion

        #region 泛型集合转换DataSet(仅list参数)
        /// <summary> 
        /// 泛型集合转换DataSet(仅list参数)
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="list">泛型集合</param> 
        /// <remarks>
        /// CreateBy:wangjun
        /// </remarks>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(IList<T> list)
        {
            return ToDataSet<T>(list, null);
        }
        #endregion

        #region 泛型集合转换DataSet(含有"待转换属性名数组"参数)
        /// <summary> 
        /// 泛型集合转换DataSet 
        /// </summary> 
        /// <typeparam name="T">泛型实体</typeparam> 
        /// <param name="p_List">泛型集合</param> 
        /// <param name="p_PropertyName">待转换属性名数组</param>
        /// <remarks>
        /// CreateBy:wangjun
        /// </remarks>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(IList<T> p_List, params string[] p_PropertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (p_PropertyName != null)
            {
                propertyNameList.AddRange(p_PropertyName);
            }

            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        // 没有指定属性的情况下全部属性都要转换 
                        _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(p_List[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(p_List[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }

                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }

            result.Tables.Add(_DataTable);
            return result;
        }
        #endregion

        #region DataSet转换为泛型集合
        /// <summary> 
        /// DataSet转换为泛型集合
        /// </summary> 
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableIndex">待转换数据表索引</param>
        /// <remarks>
        /// CreateBy:wangjun
        /// ModBy:skysong  ModBy:2014-01-16 Content:增加注释，给if加{}
        /// ModBy:skysong  ModBy:2014-01-20 公共方法于DataTable类型
        /// </remarks>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            #region 参数判断
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
            {
                return null;
            }
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
            {
                return null;
            }
            if (p_TableIndex < 0)
            {
                p_TableIndex = 0;
            }
            #endregion

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化 
            IList<T> result = DataTableToIList<T>(p_Data);

            return result;
        }
        #endregion

        #region DataSet转换为泛型集合
        /// <summary> 
        /// DataSet转换为泛型集合
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableName">待转换数据表名称</param>
        /// <remarks>
        /// CreateBy:wangjun
        /// ModBy:skysong  ModBy:2014-01-16 Content:增加注释，给if加{}
        /// </remarks>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
            {
                return null;
            }
            if (string.IsNullOrEmpty(p_TableName))
            {
                return null;
            }

            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值 
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }

            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }
        #endregion

        #region DataTable转换为泛型集合
        /// <summary>
        /// DataTable转换为泛型集合
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="p_Data">DataTable</param>
        /// <remarks>
        /// CreateBy:skysong  CreateOn:2014-01-20
        /// </remarks>
        /// <returns></returns>
        public static IList<T> DataTableToIList<T>(DataTable p_Data)
        {
            #region 参数判断
            if (p_Data == null || p_Data.Rows.Count < 0)
            {
                return null;
            }
            #endregion

            // 返回值初始化 
            IList<T> result = new List<T>();

            #region 循环赋值
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                //使用反射获取实体
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.ToLower().Equals(p_Data.Columns[i].ColumnName.ToLower()))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value)
                            {
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            }
                            else
                            {
                                pi.SetValue(_t, null, null);
                            }

                            break;
                        }
                    }
                }

                result.Add(_t);
            }
            #endregion

            return result;
        }
        #endregion
    }
}