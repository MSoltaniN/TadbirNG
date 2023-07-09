using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    public class ApiControllerTestBase<TController> : IDisposable
        where TController : Controller
    {
        protected ControllerContext TestControllerContext
        {
            get { return GetTestContext(); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _controller.Dispose();
                _disposed = true;
            }
        }

        protected void AssertActionRouteEquals(string actionName, string expectedRoute)
        {
            var method = typeof(TController).GetMethod(actionName);
            var route = Reflector.GetAttribute(method, typeof(RouteAttribute)) as RouteAttribute;

            // Assert
            Assert.That(route, Is.Not.Null);
            Assert.That(route.Template, Is.EqualTo(expectedRoute));
        }

        protected void AssertActionRouteEquals(string actionName, int paramCount, string expectedRoute)
        {
            var method = typeof(TController)
                .GetMethods()
                .Where(info => info.Name == actionName && info.GetParameters().Length == paramCount)
                .SingleOrDefault();
            var route = Reflector.GetAttribute(method, typeof(RouteAttribute)) as RouteAttribute;

            // Assert
            Assert.That(route, Is.Not.Null);
            Assert.That(route.Template, Is.EqualTo(expectedRoute));
        }

        protected void AssertActionIsSecured(string actionName)
        {
            var method = typeof(TController).GetMethod(actionName);
            var attribute = Reflector.GetAttribute(method, typeof(AuthorizeRequestAttribute))
                as AuthorizeRequestAttribute;

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        protected void AssertActionIsSecured(string actionName, string entity, int permission)
        {
            var method = typeof(TController).GetMethod(actionName);
            var attribute = Reflector.GetAttribute(method, typeof(AuthorizeRequestAttribute))
                as AuthorizeRequestAttribute;

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Entity, Is.EqualTo(entity));
            Assert.That(attribute.Permission, Is.EqualTo(permission));
        }

        protected void AssertActionHasVerbAttribute<T>(string actionName)
            where T : Attribute
        {
            var method = typeof(TController).GetMethod(actionName);
            var attribute = Reflector.GetAttribute(method, typeof(T)) as T;

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        protected ControllerContext GetTestContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[AppConstants.ContextHeaderName] = _testTicket.Value;
            httpContext.Request.Headers[AppConstants.GridOptionsHeaderName] = _testGridOptions.Value;
            return new ControllerContext() { HttpContext = httpContext };
        }

        protected TController _controller;
        private readonly Lazy<string> _testTicket = new(() =>
        {
            var appContext = new SecurityContext(new UserContextViewModel()
            {
                BranchId = 1,
                CompanyId = 1,
                FiscalPeriodId = 1,
                Id = 2,                     // User (Id = 1) is reserved for Admin user.
                PersonFullName = "Test User",
                Connection = "Test Connection"
            });
            var ticket = Transform.ToBase64String(
                Encoding.UTF8.GetBytes(
                    JsonHelper.From(appContext, false)));
            return ticket;
        });
        private readonly Lazy<string> _testGridOptions = new(() =>
        {
            var gridOptions = new GridOptions();
            var encodedOptions = Transform.ToBase64String(
                Encoding.UTF8.GetBytes(
                    WebUtility.UrlEncode(
                        JsonHelper.From(gridOptions))));
            return encodedOptions;
        });
        private bool _disposed;
    }
}
