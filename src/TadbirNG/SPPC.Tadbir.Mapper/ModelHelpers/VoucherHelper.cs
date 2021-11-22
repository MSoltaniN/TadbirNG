using System;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Mapper.ModelHelpers
{
    internal static class VoucherHelper
    {
        internal static decimal GetDebitSum(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            return voucher.Lines
                .Sum(line => line.Debit);
        }

        internal static decimal GetCreditSum(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            return voucher.Lines
                .Sum(line => line.Credit);
        }

        internal static string GetBalanceStatus(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            var debitSum = GetDebitSum(voucher);
            var creditSum = GetCreditSum(voucher);
            return (debitSum == creditSum) ? "Balanced" : "Unbalanced";
        }

        internal static string GetTypeName(Voucher voucher)
        {
            string typeName = SubjectType.Normal.ToString();
            switch (voucher.SubjectType)
            {
                case (short)SubjectType.Draft:
                    typeName = SubjectType.Draft.ToString();
                    break;
                case (short)SubjectType.Budgeting:
                    typeName = SubjectType.Budgeting.ToString();
                    break;
                default:
                    break;
            }

            return typeName;
        }
    }
}
