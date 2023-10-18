using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tools.Model;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsModelSeeder<TModel>
    {
        public CsModelSeeder(IEnumerable<TModel> seeds , bool modelIsDuplicateInSysDB)
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            _seeds = seeds;
            _modelType = typeof(TModel);
            _modelIsDuplicateInSysDB = modelIsDuplicateInSysDB;
        }

        private string GetFormattedValue(PropertyInfo property, object seed)
        {
            if (property.GetValue(seed)! == null)
                return "null";

            if (property.PropertyType == typeof(string))
            {
                return $"\"{property.GetValue(seed)}\"";
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                return $"DateTime.Parse(\"{(DateTime)property.GetValue(seed)!}\")";
            }
            else if (property.PropertyType == typeof(int))
            {
                return string.IsNullOrEmpty(property.GetValue(seed)?.ToString()) ? "0" : property.GetValue(seed)?.ToString();
            }
            else if (property.PropertyType == typeof(Guid))
            {
                return $"new Guid(\"{(Guid)property.GetValue(seed)!}\")";
            }
            else if (property.PropertyType == typeof(bool))
            {
                return property.GetValue(seed)?.ToString()?.ToLower();
            }
            else
            {
                return property.GetValue(seed)?.ToString();
            }
        }

        private IEnumerable<PropertyInfo> GetFilteredProperties()
        {
            return _modelType.GetProperties()
                 .Where(p => !p.PropertyType.FullName!.Contains("SPPC") && p.Name != "RowGuid" && p.Name != "ModifiedDate" && p.Name != "Children" && p.Name != "State" && p.Name != "Permissions" 
                                                                                    && p.Name != "RolePermissions" && p.Name != "Columns" && p.Name != "ResourceMap")
                 .OrderBy(p=>p.Name != "Id").ToList();
        }

        private readonly IEnumerable<TModel> _seeds;
        private readonly Type _modelType;
        private readonly string _version;
        private readonly bool _modelIsDuplicateInSysDB;
    }



}
