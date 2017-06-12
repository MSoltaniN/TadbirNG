using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مرتبط با مشاهده کارتابل ها را پیاده سازی می کند. 
    /// </summary>
    public class CartableService : ICartableService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public CartableService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// کارهای موجود در کارتابل ورودی یک کاربر خاص را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از رکوردهای اطلاعاتی در کارتابل ورودی کاربر داده شده</returns>
        public IEnumerable<InboxItemViewModel> GetUserInbox(int userId)
        {
            var workItems = _apiClient.Get<IEnumerable<InboxItemViewModel>>(CartableApi.UserInboxItems, userId);
            return workItems;
        }

        /// <summary>
        /// کارهای موجود در کارتابل ارسالی یک کاربر خاص را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از رکوردهای اطلاعاتی در کارتابل ارسالی کاربر داده شده</returns>
        public IEnumerable<OutboxItemViewModel> GetUserOutbox(int userId)
        {
            var workItems = _apiClient.Get<IEnumerable<OutboxItemViewModel>>(CartableApi.UserOutboxItems, userId);
            return workItems;
        }

        private IApiClient _apiClient;
    }
}
