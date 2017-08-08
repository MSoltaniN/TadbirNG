using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public class VoucherLineDependsViewModel
    {
        public VoucherLineDependsViewModel()
        {
            Accounts = new List<KeyValue>();
            DetailAccounts = new List<KeyValue>();
            CostCenters = new List<KeyValue>();
            Projects = new List<KeyValue>();
            Products = new List<KeyValue>();
            Units = new List<KeyValue>();
            Warehouses = new List<KeyValue>();
        }

        public List<KeyValue> Accounts { get; protected set; }
        public List<KeyValue> DetailAccounts { get; protected set; }
        public List<KeyValue> CostCenters { get; protected set; }
        public List<KeyValue> Projects { get; protected set; }
        public List<KeyValue> Products { get; protected set; }
        public List<KeyValue> Units { get; protected set; }
        public List<KeyValue> Warehouses { get; protected set; }
    }
}
