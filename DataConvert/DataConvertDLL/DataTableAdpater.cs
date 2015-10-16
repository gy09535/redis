using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DataConvertDLL
{

    /// <summary>
    /// 用于数据转换的适配器(引用Gof适配器设计模式),若对其他类型进行转换只需实现此接口,再使用工厂调用
    /// </summary>
    public interface IDataAdapter<TSource> where TSource : class
    {
        ICollection<TConvert> DataConvert<TConvert>(TSource objSource) where TConvert : class, new()
           ;
    }


    /// <summary>
    /// 用于DataTable转化的适配器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataTableAdapter : Attribute, IDataAdapter<DataTable>
    {
        public ICollection<TConvert> DataConvert<TConvert>(DataTable objSource) where TConvert : class, new()
        {
            if (objSource == null) return null;
            var lists = new List<TConvert>();
            try
            {
                var objType = typeof(TConvert);
                foreach (DataRow dr in objSource.Rows)
                {
                    var obj = System.Activator.CreateInstance<TConvert>();
                    foreach (PropertyInfo objProperty in objType.GetProperties())
                    {
                        if (objSource.Columns.Contains(objProperty.Name) && !(dr[objProperty.Name] is DBNull))
                        {
                            objProperty.SetValue(obj, dr[objProperty.Name], null);
                            continue;
                        }
                        foreach (var attribute in objProperty.GetCustomAttributes(false))
                        {
                            var tableColumnName = attribute as TableColumnName;
                            if (tableColumnName != null && objSource.Columns.Contains(tableColumnName.ColumnName) && !(dr[tableColumnName.ColumnName] is DBNull))
                            {
                                objProperty.SetValue(obj, dr[tableColumnName.ColumnName], null);
                                break;
                            }
                        }

                    }
                    lists.Add(obj);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lists;
        }
    }


    public abstract class ConvertBase : Attribute
    {
        public abstract string ColumnName { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class TableColumnName : ConvertBase
    {
        public override string ColumnName { get; set; }

        public TableColumnName(string columnName)
        {
            ColumnName = columnName;
        }
    }

    /// <summary>
    /// 使用Gof抽象工厂设计模式
    /// </summary>
    public interface IConvertFactory
    {
        void Fill<TConvert, TSource>(ref ICollection<TConvert> convert, TSource source)
            where TConvert : class, new()
            where TSource : class, new();
    }

    /// <summary>
    /// 使用Gof抽象工厂设计模式根据相应的转换类型进行转换
    /// </summary>
    public class DataAdapterFactory : IConvertFactory
    {

        public void Fill<TConvert, TSource>(ref ICollection<TConvert> convert, TSource source)
            where TConvert : class, new()
            where TSource : class, new()
        {
            try
            {
                var type = typeof(TConvert);
                foreach (var attribute in type.GetCustomAttributes(false))
                {
                    if (attribute is IDataAdapter<TSource>)
                    {
                        var adapter = attribute as IDataAdapter<TSource>;
                        convert = adapter.DataConvert<TConvert>(source);
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
