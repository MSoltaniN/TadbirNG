using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for managing required contextual information related to current application user.
    /// This class is intended to be used for authorization at API service level; i.e. in Web API service controllers.
    /// </summary>
    public class ServiceContextManager : ISecurityContextManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=" ServiceContextManager"/> class, using given Web context.
        /// </summary>
        /// <param name="httpContextAccessor">An object that provides current Web application context</param>
        /// <param name="contextEncoder">An object used for encoding and decoding <see cref="SecurityContext"/> objects</param>
        public ServiceContextManager(IHttpContextAccessor httpContextAccessor, ITextEncoder<SecurityContext> contextEncoder)
        {
            _httpContextAccessor = httpContextAccessor;
            _contextEncoder = contextEncoder;
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        /// Gets current security context at service level, usually set after a successful login at application level.
        /// </summary>
        public ISecurityContext CurrentContext
        {
            get { return GetCurrentContext(); }
        }

        /// <summary>
        /// Gets current security context in a readable encoded form.
        /// </summary>
        public string EncodedContext
        {
            get { return GetEncodedContext(); }
        }

        /// <summary>
        /// Saves security context of current application user in an HTTP request header, so that it can be
        /// easily retrieved on demand for performing authorization.
        /// </summary>
        /// <param name="userContext">Object containing context information related to current application user</param>
        public void SetUserContext(UserContextViewModel userContext)
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            var context = new SecurityContext(userContext);
            _httpContext.Response.Headers.Add(AppConstants.ContextHeaderName, _contextEncoder.Encode(context));
        }

        private ISecurityContext GetCurrentContext()
        {
            ISecurityContext current = null;
            if (_httpContext.Request.Headers.ContainsKey(AppConstants.ContextHeaderName))
            {
                current = _contextEncoder.Decode(_httpContext.Request.Headers[AppConstants.ContextHeaderName]);
            }

            return current;
        }

        private string GetEncodedContext()
        {
            string current = String.Empty;
            if (_httpContext.Request.Headers.ContainsKey(AppConstants.ContextHeaderName))
            {
                current = _httpContext.Request.Headers[AppConstants.ContextHeaderName];
            }

            return current;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly ITextEncoder<SecurityContext> _contextEncoder;
    }
}
