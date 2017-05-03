using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    public class CartableService : ICartableService
    {
        public CartableService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<InboxItemViewModel> GetUserInbox(int userId)
        {
            var workItems = _apiClient.Get<IEnumerable<InboxItemViewModel>>(CartableApi.UserInboxItems, userId);
            return workItems;
        }

        private IApiClient _apiClient;
    }
}
