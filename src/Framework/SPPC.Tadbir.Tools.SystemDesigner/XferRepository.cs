using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BabakSoft.Platform.Data;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    public class XferRepository
    {
        public XferRepository(string sourceConnection, string targetConnection)
        {
            _dalFrom = new SqlDataLayer(sourceConnection, ProviderType.SqlClient);
            _dalTo = new SqlDataLayer(targetConnection, ProviderType.SqlClient);
        }

        public void XferFiscalPeriods()
        {
            var fps = _dalFrom.Query("SELECT * FROM __FiscalPeriod__ WHERE Id > 0");
            foreach (DataRow row in fps.Rows)
            {
                string insertQuery = String.Format(Scripts.InsertFiscalPeriod,
                    row["Id"].ToString(), row["Name"].ToString(), row["StartDate"].ToString(), row["EndDate"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }

            _dalTo.ExecuteNonQuery(Scripts.InsertDefaultBranch);
            _dalTo.ExecuteNonQuery(Scripts.InsertDefaultCurrency);
        }

        public void XferAccountGroups()
        {
            var groups = _dalFrom.Query("SELECT * FROM __SysGrpAcc__ WHERE Id > 0");
            foreach (DataRow row in groups.Rows)
            {
                string insertQuery = String.Format(Scripts.InsertAccountGroup,
                    row["Id"].ToString(), row["Name"].ToString(), row["SType"].ToString(), row["Categ"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferAccounts()
        {
            var accounts = _dalFrom.Query("SELECT * FROM __Account__ WHERE Id > 0");
            _accountMap = BuildAccountMap(accounts);
            foreach (DataRow row in accounts.Rows)
            {
                var vm = GetViewModel(row, _accountMap);
                string insertQuery = String.Format(Scripts.InsertAccount,
                    vm.Id, vm.ParentId.HasValue ? vm.ParentId.Value.ToString() : "NULL",
                    vm.GroupId.HasValue ? vm.GroupId.Value.ToString() : "NULL", vm.FiscalPeriodId,
                    vm.Code, vm.FullCode, vm.Name, vm.Level);
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferDetailAccounts()
        {
            var detailAccounts = _dalFrom.Query("SELECT * FROM __DetailAcc__ WHERE Id > 0");
            var vms = detailAccounts.Rows
                .Cast<DataRow>()
                .Select(row => GetViewModel(row))
                .ToList();
            foreach (DataRow row in detailAccounts.Rows)
            {
                var vm = GetViewModel(row);
                if (vm.Level > 0)
                {
                    var parent = vms
                        .Where(item => item.FullCode == vm.FullCode.Substring(0, vm.FullCode.Length - vm.Code.Length))
                        .First();
                    vm.ParentId = parent.Id;
                }

                string insertQuery = String.Format(Scripts.InsertDetailAccount,
                    vm.Id,
                    vm.ParentId.HasValue ? vm.ParentId.Value.ToString() : "NULL",
                    vm.FiscalPeriodId, vm.Code, vm.FullCode, vm.Name, vm.Level);
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferCostCenters()
        {
            var centers = _dalFrom.Query("SELECT * FROM __CostCenter__ WHERE Id > 0");
            foreach (DataRow row in centers.Rows)
            {
                string insertQuery = String.Format(Scripts.InsertCostCenter,
                    row["Id"].ToString(), row["FPId"].ToString(), row["CCCode"].ToString(),
                    row["CCCode"].ToString(), row["Name"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferProjects()
        {
            var projects = _dalFrom.Query("SELECT * FROM __Project__ WHERE Id > 0");
            foreach (DataRow row in projects.Rows)
            {
                string insertQuery = String.Format(Scripts.InsertProject,
                    row["Id"].ToString(), row["FPId"].ToString(), row["PCode"].ToString(),
                    row["PCode"].ToString(), row["Name"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferVouchers()
        {
            var vouchers = _dalFrom.Query("SELECT * FROM __Transaction__ WHERE Id > 0");
            foreach (DataRow row in vouchers.Rows)
            {
                string date = DateTime.Now.Parse(row["TDate"].ToString(), true).ToShortDateString(false);
                string insertQuery = String.Format(Scripts.InsertVoucher,
                    row["Id"].ToString(), row["FPId"].ToString(), row["SanadNo"].ToString(),
                    date, row["TRes"].ToString(), row["TDesc"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        public void XferVoucherLines()
        {
            int id = 1;
            var vouchers = _dalFrom.Query("SELECT * FROM __Article__ WHERE Id > 0 ORDER BY TransId, Id");
            foreach (DataRow row in vouchers.Rows)
            {
                if (row["AccountId"].ToString() == "0")
                {
                    continue;
                }

                string accountId = _accountMap[row["AccountId"].ToString()].ToString();
                string detailId = row["FAccId"].ToString() == "0" ? "NULL" : row["FAccId"].ToString();
                string centerId = row["CostCenter"].ToString() == "0" ? "NULL" : row["CostCenter"].ToString();
                string projectId = row["Project"].ToString() == "0" ? "NULL" : row["Project"].ToString();
                string insertQuery = String.Format(Scripts.InsertVoucherLine,
                    id++, row["TransId"].ToString(), row["FPId"].ToString(),
                    accountId, detailId, centerId, projectId,
                    row["ADesc"].ToString(), row["Debit"].ToString(), row["Credit"].ToString());
                _dalTo.ExecuteNonQuery(insertQuery);
            }
        }

        // WARNING: For this logic to work properly, source database MUST have ONLY ONE fiscal period.
        private Dictionary<string, int> BuildAccountMap(DataTable accounts)
        {
            int id = 1;
            var map = new Dictionary<string, int>();
            for (int i = 0; i < accounts.Rows.Count; i++)
            {
                var row = accounts.Rows[i];
                row["Id"] = id;
                map.Add(row["FullId"].ToString(), id);
                id++;
            }

            return map;
        }

        private AccountViewModel GetViewModel(DataRow row, IDictionary<string, int> accountMap)
        {
            var account = new AccountViewModel()
            {
                FiscalPeriodId = Int32.Parse(row["FPId"].ToString()),
                FullCode = row["FullId"].ToString(),
                Id = Int32.Parse(row["Id"].ToString()),
                Level = (short)(Int16.Parse(row["AccLevel"].ToString()) - 1),
                Name = row["Name"].ToString()
            };

            if (row["Category"].ToString() != "0")
            {
                account.GroupId = Int32.Parse(row["Category"].ToString());
            }

            string parentCode = row["ParentId"].ToString();
            if (parentCode != "0")
            {
                account.Code = account.FullCode.Substring(parentCode.Length);
                account.ParentId = accountMap[parentCode];
            }
            else
            {
                account.Code = account.FullCode;
            }

            return account;
        }

        private DetailAccountViewModel GetViewModel(DataRow row)
        {
            var viewModel = new DetailAccountViewModel()
            {
                FiscalPeriodId = Int32.Parse(row["FPId"].ToString()),
                Id = Int32.Parse(row["Id"].ToString()),
                Level = (short)(Int32.Parse(row["AccLevel"].ToString()) - 4),
                Name = row["Name"].ToString()
            };

            string code = String.Empty, fullCode = String.Empty;
            for (int i = 0; i <= viewModel.Level; i++)
            {
                string fieldName = String.Format("T{0}", i + 1);
                code = row[fieldName].ToString();
                fullCode += code;
            }

            viewModel.Code = code;
            viewModel.FullCode = fullCode;
            return viewModel;
        }

        private SqlDataLayer _dalFrom;
        private SqlDataLayer _dalTo;
        private Dictionary<string, int> _accountMap;
    }
}
