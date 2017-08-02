using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using BabakSoft.Platform.Configuration;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Service
{
    /// <summary>
    /// Provides common operations for working with a Web API service.
    /// </summary>
    public class ServiceClient : IApiClient, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <remarks>
        /// This utility class requires the root URL of Web API service to be specified in an appSettings entry
        /// inside main configuration file of the client assembly. The key for this entry must be 'ServiceRoot'
        /// and the value must be the full URL of service root.
        /// </remarks>
        public ServiceClient()
        {
            var root = ConfigHelper.GetAppSettings("ServiceRoot");
            _httpClient = new HttpClient() { BaseAddress = new Uri(root), Timeout = Timeout.InfiniteTimeSpan };
        }

        /// <summary>
        /// Retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        public T Get<T>(string apiUrl, params object[] apiUrlArgs)
        {
            T value = default(T);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.GetAsync(url).Result;
            var serviceResponse = GetResponse(response);
            if (serviceResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = Json.To<T>(response.Content.ReadAsStringAsync().Result);
            }

            return value;
        }

        /// <summary>
        /// Inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public ServiceResponse Insert<T>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.PostAsJsonAsync(url, data).Result;
            return GetResponse(response);
        }

        /// <summary>
        /// Updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public ServiceResponse Update<T>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.PutAsJsonAsync(url, data).Result;
            return GetResponse(response);
        }

        /// <summary>
        /// Deletes data by sending an HTTP DELETE request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public ServiceResponse Delete(string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.DeleteAsync(url).Result;
            return GetResponse(response);
        }

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private static ServiceResponse GetResponse(HttpResponseMessage response)
        {
            var serviceResponse = new ServiceResponse();
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = response.Content.ReadAsStringAsync().Result;
                var serviceMessage = Json.To<ServiceMessage>(message);
                serviceResponse = new ServiceResponse(ServiceResult.ValidationFailed, serviceMessage.Message);
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                serviceResponse = new ServiceResponse(ServiceResult.ServerError, String.Empty);
            }
            else if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Error occurred while executing Web API request.");
            }

            return serviceResponse;
        }

        private Uri GetApiResourceUrl(string apiResource, params object[] args)
        {
            var resourceUrl = String.Format(apiResource, args);
            return new Uri(String.Format("{0}{1}", _httpClient.BaseAddress.ToString(), resourceUrl));
        } 

        /// <summary>
        /// Internal object used for sending HTTP requests
        /// </summary>
        protected HttpClient _httpClient;
        private bool _disposed = false;
    }
}
