using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tools.TadbirDbConverter
{
    public class TadbirMapper : IDomainMapper
    {
        static TadbirMapper()
        {
            _configuration = new MapperConfiguration(config => RegisterMappings(config));
            _autoMapper = _configuration.CreateMapper();
            _map = JsonHelper.To<IDictionary<string, TableMapping>>(File.ReadAllText(_mappingPath));
        }

        public object Configuration
        {
            get { return _configuration; }
        }

        public T Map<T>(object source)
        {
            return _autoMapper.Map<T>(source);
        }

        private static void RegisterMappings(IMapperConfigurationExpression mapperConfig)
        {
            MapFinanceTypes(mapperConfig);
        }

        private static void MapFinanceTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<DataRow, FiscalPeriod>()
                .ConstructUsing(row => MapModel<FiscalPeriod>(row));
        }

        private static TModel MapModel<TModel>(DataRow row)
            where TModel : class, new()
        {
            var model = new TModel();
            var typeName = typeof(TModel).Name.CamelCase();
            if (_map.ContainsKey(typeName))
            {
                var tableMapping = _map[typeName];
                var mainColumns = row.Table.Columns
                    .Cast<DataColumn>()
                    .Where(col => !tableMapping.IgnoreFields.Contains(col.ColumnName));
                foreach (DataColumn column in mainColumns)
                {
                    var mapping = tableMapping.Fields
                        .Where(fld => fld.FromName == column.ColumnName)
                        .FirstOrDefault();
                    var toName = mapping != null ? mapping.ToName : column.ColumnName;
                    var value = ValueOrDefault(column.DataType, row, column.ColumnName);
                    Reflector.SetProperty(model, toName, value);
                }
            }

            return model;
        }

        public static object ValueOrDefault(Type type, DataRow row, string field)
        {
            object value = null;
            if (row.Table.Columns.Contains(field) && row[field] != DBNull.Value)
            {
                value = Convert.ChangeType(row[field], type);
            }

            return value;
        }

        private const string _mappingPath = @"..\..\..\src\TadbirNG\SPPC.Tools.TadbirDbConverter\convert-mapping.json";
        private static readonly IDictionary<string, TableMapping> _map;
        private static readonly MapperConfiguration _configuration;
        private static readonly IMapper _autoMapper;
    }
}
