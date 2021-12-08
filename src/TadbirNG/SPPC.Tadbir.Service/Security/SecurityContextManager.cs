using System;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Provides operations for managing required contextual information related to current application user.
    /// This class is intended to be used for authorization at application level; i.e. in application views
    /// and controllers.
    /// </summary>
    public class SecurityContextManager : ISecurityContextManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref=" SecurityContextManager"/> class, using given Web context.
        /// </summary>
        /// <param name="httpContextAccessor">An object that provides current Web application context</param>
        /// <param name="serializer">Serializer used for encoding and decoding objects</param>
        public SecurityContextManager(IHttpContextAccessor httpContextAccessor, IEncodedSerializer serializer)
        {
            _serializer = serializer;
            _httpContext = httpContextAccessor.HttpContext;
            _rootUrl = AppConstants.AppRoot;
        }

        /// <summary>
        /// Gets current security context, usually set after a successful login.
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
        /// Saves security context of current application user in an HTTP cookie, so that it can be
        /// easily retrieved on demand for performing authorization.
        /// </summary>
        /// <param name="userContext">Object containing context information related to current application user;
        /// If this object is null, this method immediately expires context cookie, effectively clearing the context.
        /// </param>
        public void SetUserContext(UserContextViewModel userContext)
        {
            if (userContext == null)
            {
                ClearContext();
                return;
            }

            var context = new SecurityContext(userContext);
            _httpContext.Response.Cookies.Append(
                AppConstants.ContextCookieName,
                _serializer.Serialize(context),
                new CookieOptions() { Path = _rootUrl });
        }

        private ISecurityContext GetCurrentContext()
        {
            ISecurityContext current = null;
            var contextCookie = _httpContext.Request.Cookies[AppConstants.ContextCookieName];
            if (contextCookie != null)
            {
                current = _serializer.Deserialize<SecurityContext>(contextCookie);
            }

            return current;
        }

        private string GetEncodedContext()
        {
            var contextCookie = _httpContext.Request.Cookies[AppConstants.ContextCookieName];
            return contextCookie ?? String.Empty;
        }

        private void ClearContext()
        {
            _httpContext.Response.Cookies.Delete(AppConstants.ContextCookieName);
        }

        private readonly HttpContext _httpContext;
        private readonly IEncodedSerializer _serializer;
        private readonly string _rootUrl;
    }
}
