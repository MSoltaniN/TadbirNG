using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using SPPC.Tadbir.ViewModel.Auth;
using SwForAll.Platform.Configuration;

namespace SPPC.Tadbir.Service
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
        /// <param name="httpContext">An object that provides current Web application context</param>
        /// <param name="contextEncoder">An object used for encoding and decoding <see cref="SecurityContext"/> objects</param>
        public SecurityContextManager(HttpContextBase httpContext, ITextEncoder<SecurityContext> contextEncoder)
        {
            _httpContext = httpContext;
            _contextEncoder = contextEncoder;
            _rootUrl = ConfigHelper.GetAppSettings(Values.Constants.AppRootKey);
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
            var cookie = new HttpCookie(Values.Constants.ContextCookieName, _contextEncoder.Encode(context));
            cookie.Path = _rootUrl;
            _httpContext.Response.Cookies.Set(cookie);
        }

        private ISecurityContext GetCurrentContext()
        {
            ISecurityContext current = null;
            var contextCookie = _httpContext.Request.Cookies[Values.Constants.ContextCookieName];
            if (contextCookie != null)
            {
                current = _contextEncoder.Decode(contextCookie.Value);
            }

            return current;
        }

        private string GetEncodedContext()
        {
            string current = String.Empty;
            var contextCookie = _httpContext.Request.Cookies[Values.Constants.ContextCookieName];
            if (contextCookie != null)
            {
                current = contextCookie.Value;
            }

            return current;
        }

        private void ClearContext()
        {
            var cookie = _httpContext.Request.Cookies[Values.Constants.ContextCookieName];
            cookie.Expires = DateTime.Now.AddDays(-1);
            _httpContext.Response.Cookies.Set(cookie);
        }

        private readonly HttpContextBase _httpContext;
        private readonly ITextEncoder<SecurityContext> _contextEncoder;
        private readonly string _rootUrl;
    }
}
