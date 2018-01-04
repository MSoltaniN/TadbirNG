using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// عملیات مرتبط با ردگیری  گردش های کاری را پیاده سازی می کند.
    /// </summary>
    public class WorkflowTracker : IWorkflowTracker
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="settingsRepository">پیاده سازی اینترفیس مربوط به ذخیره و بازیابی تنظیمات</param>
        /// <param name="workflowRepository">پیاده سازی اینترفیس مربوط به ذخیره و بازیابی اطلاعات گردش های کاری</param>
        public WorkflowTracker(ISettingsRepository settingsRepository, IWorkflowRepository workflowRepository)
        {
            _settingsRepository = settingsRepository;
            _workflowRepository = workflowRepository;
        }

        /// <summary>
        /// ویرایش گردش کاری را برای گردش کار متناظر با یک مستند برمی گرداند
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند موجود</param>
        /// <param name="documentType">نوع مستند مورد نظر</param>
        /// <returns></returns>
        public string TrackDocumentWorkflowEdition(int documentId, string documentType)
        {
            string edition = String.Empty;
            var instance = _workflowRepository.GetRunningInstance(documentId, documentType);
            if (instance != null)
            {
                edition = instance.EditionName;
            }
            else
            {
                var editionViewModel = _settingsRepository.GetDefaultWorkflowEdition(WorkflowTitle.TransactionState);
                edition = editionViewModel.Name;
            }

            return edition;
        }

        private ISettingsRepository _settingsRepository;
        private IWorkflowRepository _workflowRepository;
    }
}
