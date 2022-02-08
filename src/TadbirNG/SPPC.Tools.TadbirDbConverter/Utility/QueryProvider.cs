using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;

namespace SPPC.Tools.TadbirDbConverter
{
    public class QueryProvider
    {
        public string GetSelectQuery<TModel>()
            where TModel : class, new()
        {
            var selectQuery = String.Empty;
            var typeName = typeof(TModel).Name.CamelCase();
            //if (_map.ContainsKey(typeName))
            //{
            //    var mapping = _map[typeName];
            //    var queryBuilder = new StringBuilder();
            //    queryBuilder.AppendFormat($"SELECT * FROM [dbo].[{mapping.SourceTable}]");
            //    queryBuilder.AppendFormat($" WHERE Id > 0");
            //    if (mapping.HadFpId)
            //    {
            //        queryBuilder.AppendFormat($" AND FPId > 0");
            //    }

            //    selectQuery = queryBuilder.ToString();
            //}

            return selectQuery;
        }
    }
}
