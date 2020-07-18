using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    public partial class VoucherRepository
    {
        /// <summary>
        /// عمل داده شده را روی سند با شناسه دیتابیسی مشخص شده بررسی و اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="action">عمل مورد نظر</param>
        /// <returns>در صورت مجاز بودن عمل، مقدار خالی و در غیر این صورت پیغام خطا را برمی گرداند
        /// </returns>
        public async Task<GroupActionResultViewModel> ValidateVoucherActionAsync(int voucherId, string action)
        {
            string error = String.Empty;
            var voucher = await GetVoucherAsync(voucherId);
            if (action == AppStrings.Check)
            {
                error = ValidateCheck(voucher);
            }
            else if (action == AppStrings.UndoCheck)
            {
                error = ValidateUndoCheck(voucher);
            }
            else if (action == AppStrings.Confirm)
            {
                error = ValidateConfirm(voucher);
            }
            else if (action == AppStrings.UndoConfirm)
            {
                error = ValidateUndoConfirm(voucher);
            }
            else if (action == AppStrings.Approve)
            {
                error = ValidateApprove(voucher);
            }
            else if (action == AppStrings.UndoApprove)
            {
                error = ValidateUndoApprove(voucher);
            }
            else if (action == AppStrings.Finalize)
            {
                error = ValidateFinalize(voucher);
            }
            else if (action == AppStrings.UndoFinalize)
            {
                error = ValidateUndoFinalize(voucher);
            }

            return String.IsNullOrEmpty(error)
                ? null
                : new GroupActionResultViewModel()
                {
                    Id = voucher.Id,
                    Date = voucher.Date,
                    ErrorMessage = error,
                    No = voucher.No
                };
        }

        private string ValidateCheck(VoucherViewModel voucher)
        {
            string error = String.Empty;
            if (!voucher.IsBalanced)
            {
                var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Check, AppStrings.BalanceLabel));
            }
            else if (voucher.StatusId != (int)DocumentStatusValue.Draft)
            {
                var template = Context.Localize(AppStrings.RepeatedVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Check));
            }

            return error;
        }

        private string ValidateUndoCheck(VoucherViewModel voucher)
        {
            string error = String.Empty;
            var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
            if (voucher.IsApproved)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoCheck, AppStrings.UndoApprove));
            }
            else if (voucher.IsConfirmed)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoCheck, AppStrings.UndoConfirm));
            }
            else if (voucher.StatusId != (int)DocumentStatusValue.Checked)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoCheck, AppStrings.Check));
            }

            return error;
        }

        private string ValidateConfirm(VoucherViewModel voucher)
        {
            string error = String.Empty;
            if (voucher.IsConfirmed)
            {
                var template = Context.Localize(AppStrings.RepeatedVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Confirm));
            }
            else if (voucher.StatusId == (int)DocumentStatusValue.Draft)
            {
                var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Confirm, AppStrings.Check));
            }

            return error;
        }

        private string ValidateUndoConfirm(VoucherViewModel voucher)
        {
            string error = String.Empty;
            var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
            if (voucher.IsApproved)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoConfirm, AppStrings.UndoApprove));
            }
            else if (!voucher.IsConfirmed)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoConfirm, AppStrings.Confirm));
            }

            return error;
        }

        private string ValidateApprove(VoucherViewModel voucher)
        {
            string error = String.Empty;
            if (voucher.IsApproved)
            {
                var template = Context.Localize(AppStrings.RepeatedVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Approve));
            }
            else if (!voucher.IsConfirmed)
            {
                var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Approve, AppStrings.Confirm));
            }

            return error;
        }

        private string ValidateUndoApprove(VoucherViewModel voucher)
        {
            string error = String.Empty;
            var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
            if (!voucher.IsApproved)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoApprove, AppStrings.Approve));
            }

            return error;
        }

        private string ValidateFinalize(VoucherViewModel voucher)
        {
            string error = String.Empty;
            if (voucher.StatusId == (int)DocumentStatusValue.Finalized)
            {
                var template = Context.Localize(AppStrings.RepeatedVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Finalize));
            }
            else if (voucher.StatusId == (int)DocumentStatusValue.Draft)
            {
                var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
                error = Context.Localize(String.Format(template, AppStrings.Finalize, AppStrings.Check));
            }

            return error;
        }

        private string ValidateUndoFinalize(VoucherViewModel voucher)
        {
            string error = String.Empty;
            var template = Context.Localize(AppStrings.InvalidVoucherActionMessage);
            if (voucher.IsApproved)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoFinalize, AppStrings.UndoApprove));
            }
            else if (voucher.IsConfirmed)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoFinalize, AppStrings.UndoConfirm));
            }
            else if (voucher.StatusId != (int)DocumentStatusValue.Finalized)
            {
                error = Context.Localize(String.Format(template, AppStrings.UndoFinalize, AppStrings.Finalize));
            }

            return error;
        }
    }
}
