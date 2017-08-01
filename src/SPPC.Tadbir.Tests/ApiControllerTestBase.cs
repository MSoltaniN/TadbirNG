using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using NUnit.Framework;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    public class ApiControllerTestBase<TController> : IDisposable
        where TController : ApiController
    {
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

        protected TController _controller;
        private bool _disposed;
    }
}
