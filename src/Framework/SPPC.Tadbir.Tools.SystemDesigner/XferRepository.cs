using System;
using System.Collections.Generic;
using System.Data;
using BabakSoft.Platform.Data;
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
            Dictionary<string, int> accountMap = BuildAccountMap(accounts);
            foreach (DataRow row in accounts.Rows)
            {
                var vm = GetViewModel(row, accountMap);
                string insertQuery = String.Format(Scripts.InsertAccount,
                    vm.Id, vm.ParentId.HasValue ? vm.ParentId.Value.ToString() : "NULL",
                    vm.GroupId.HasValue ? vm.GroupId.Value.ToString() : "NULL", vm.FiscalPeriodId,
                    vm.Code, vm.FullCode, vm.Name, vm.Level);
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

        private SqlDataLayer _dalFrom;
        private SqlDataLayer _dalTo;
    }
}
