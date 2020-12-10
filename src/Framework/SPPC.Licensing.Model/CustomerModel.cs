using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Licensing.Model
{
    public class CustomerModel : IEntity
    {
        public CustomerModel()
        {
            Licenses = new List<LicenseModel>();
        }

        public int Id { get; set; }

        public string CustomerKey { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public string EmployeeCount { get; set; }

        public string HeadquartersAddress { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string WorkPhone { get; set; }

        public string WorkFax { get; set; }

        public string CellPhone { get; set; }

        public IList<LicenseModel> Licenses { get; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
