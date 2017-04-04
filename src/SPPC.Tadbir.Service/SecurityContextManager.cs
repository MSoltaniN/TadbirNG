using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using SPPC.Tadbir.ViewModel.Auth;
using SwForAll.Platform.Common;

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
        public SecurityContextManager(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Gets current security context, usually set after a successful login.
        /// </summary>
        public ISecurityContext CurrentContext
        {
            get { return GetCurrentContext(); }
        }

        /// <summary>
        /// Saves security context of current application user in an HTTP cookie, so that it can be
        /// easily retrieved on demand for performing authorization.
        /// </summary>
        /// <param name="userContext">Object containing context information related to current application user</param>
        public void SetUserContext(UserContextViewModel userContext)
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            var context = new SecurityContext(userContext);
            var cookie = new HttpCookie(Values.Constants.ContextCookieName, ToEncodedValue(context));
            _httpContext.Response.Cookies.Set(cookie);
        }

        private static ISecurityContext FromEncodedValue(string encodedValue)
        {
            Verify.ArgumentNotNullOrEmptyString(encodedValue, "encodedValue");
            ISecurityContext context = null;
            using (var stream = new MemoryStream(Transform.FromBase64String(encodedValue)))
            {
                var formatter = new BinaryFormatter();
                context = formatter.Deserialize(stream) as SecurityContext;
            }

            return context;
        }

        private static string ToEncodedValue(SecurityContext context)
        {
            var encodedValue = String.Empty;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, context);
                encodedValue = Transform.ToBase64String(stream.ToArray());
            }

            return encodedValue;
        }

        private ISecurityContext GetCurrentContext()
        {
            ISecurityContext current = null;
            var contextCookie = _httpContext.Request.Cookies[Values.Constants.ContextCookieName];
            if (contextCookie != null)
            {
                current = FromEncodedValue(contextCookie.Value);
            }

            return current;
        }

        private HttpContextBase _httpContext;
    }
}
