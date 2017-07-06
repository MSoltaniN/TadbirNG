using System;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// کلاس کمکی برای اعتبارسنجی عملیات مرتبط با تغییر وضعیت موجودیت های عملیاتی
    /// </summary>
    public sealed class StateOperationValidator
    {
        private StateOperationValidator()
        {
        }

        /// <summary>
        /// با توجه به نوع عملیات (اقدام) و وضعیت عملیاتی داده شده اقدام مورد درخواست را اعتبار سنجی می کند
        /// </summary>
        /// <param name="operation">نوع اقدامی که باید اعتبارسنجی شود</param>
        /// <param name="status">وضعیت عملیاتی مستندی که اقدام مورد نظر باید روی آن انجام شود</param>
        /// <returns>در صورت معتبر بودن عملیات مقدار درست
        /// true
        /// و در غیر این صورت نادرست
        /// false
        /// را برمی گرداند.</returns>
        public static bool Validate(string operation, string status)
        {
            return (_validOperations.ContainsKey(operation)
                && _validOperations[operation] == status);
        }

        private static IDictionary<string, string> _validOperations = new Dictionary<string, string>
        {
            { DocumentAction.Prepare, DocumentStatus.Created },
            { DocumentAction.Review, DocumentStatus.Prepared },
            { DocumentAction.Reject, DocumentStatus.Reviewed },
            { DocumentAction.Confirm, DocumentStatus.Reviewed },
            { DocumentAction.Approve, DocumentStatus.Confirmed }
        };
    }
}
