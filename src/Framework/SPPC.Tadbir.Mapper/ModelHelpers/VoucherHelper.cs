﻿using System;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Mapper.ModelHelpers
{
    internal static class VoucherHelper
    {
        internal static decimal GetDebitSum(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            return voucher.Lines
                .Select(line => line.Debit)
                .Sum();
        }

        internal static decimal GetCreditSum(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            return voucher.Lines
                .Select(line => line.Credit)
                .Sum();
        }

        internal static string GetBalanceStatus(Voucher voucher)
        {
            Verify.ArgumentNotNull(voucher, nameof(voucher));
            var debitSum = GetDebitSum(voucher);
            var creditSum = GetCreditSum(voucher);
            return (debitSum == creditSum) ? "Balanced" : "Unbalanced";
        }
    }
}