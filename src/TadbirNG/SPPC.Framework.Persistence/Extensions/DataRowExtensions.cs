using System;
using System.Data;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string ValueOrDefault(this DataRow row, string fieldName)
        {
            string value = null;
            if (row.Table.Columns.Contains(fieldName))
            {
                value = row[fieldName].ToString();
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T ValueOrDefault<T>(this DataRow row, string fieldName)
        {
            var value = default(T);
            if (row.Table.Columns.Contains(fieldName) && row[fieldName] != DBNull.Value)
            {
                value = (T)Convert.ChangeType(row[fieldName], typeof(T));
            }

            return value;
        }
    }
}
