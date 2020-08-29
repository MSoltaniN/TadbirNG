using System;
using SPPC.Framework.Service;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides common operations for working with a Web API service that requires authentication and authorization.
    /// </summary>
    public class ApiClient : ServiceClient, IApiClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class that uses given context manager.
        /// </summary>
        /// <param name="contextManager">An object used for security authentication and authorization of
        /// service operations</param>
        public ApiClient(ISecurityContextManager contextManager)
        {
            _contextManager = contextManager;
            InitClient();
        }

        private void InitClient()
        {
            var context = _contextManager.EncodedContext;
            if (!String.IsNullOrWhiteSpace(context))
            {
                _httpClient.DefaultRequestHeaders.Add(AppConstants.ContextHeaderName, context);
            }
        }

        private ISecurityContextManager _contextManager;
    }
}
