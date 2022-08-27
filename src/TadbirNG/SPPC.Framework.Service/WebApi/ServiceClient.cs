using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service.Extensions;
using SPPC.Framework.Values;

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
            LastResponse = new ServiceResponse();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <remarks>
        /// This utility class requires the root URL of Web API service to be specified in an appSettings entry
        /// inside main configuration file of the client assembly. The key for this entry must be 'ServiceRoot'
        /// and the value must be the full URL of service root.
        /// </remarks>
        public ServiceClient(string root)
            : this()
        {
            Verify.ArgumentNotNullOrEmptyString(root, nameof(root));
            ServiceRoot = root;
        }

        /// <summary>
        /// Gets or sets the URL for API service
        /// </summary>
        public string ServiceRoot
        {
            get
            {
                return _serviceRoot;
            }
            set
            {
                _serviceRoot = value;
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_serviceRoot),
                    Timeout = Timeout.InfiniteTimeSpan
                };
            }
        }

        /// <summary>
        /// Gets the last response received from service
        /// </summary>
        public ServiceResponse LastResponse { get; private set; }

        /// <summary>
        /// Adds a single-valued HTTP header specified by name and value to all requests
        /// </summary>
        /// <param name="name">Name of header to add</param>
        /// <param name="value">Single value to set in added header</param>
        public void AddHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Removes an HTTP header specified by name
        /// </summary>
        /// <param name="name">Name of header to remove</param>
        public void RemoveHeader(string name)
        {
            if (_httpClient.DefaultRequestHeaders.Contains(name))
            {
                _httpClient.DefaultRequestHeaders.Remove(name);
            }
        }

        /// <summary>
        /// Removes all HTTP headers
        /// </summary>
        public void RemoveAllHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Retrieves raw data from a file by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>File information as a <see cref="FileResource"/> instance, or null if the file resource
        /// could not be found</returns>
        public FileResource GetFile(string apiUrl, params object[] apiUrlArgs)
        {
            var file = default(FileResource);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.GetAsync(url).Result;
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                file = new FileResource()
                {
                    Name = response.Content.Headers.ContentDisposition.FileName,
                    Size = response.Content.Headers.ContentLength ?? 0L,
                    ContentType = response.Content.Headers.ContentType.MediaType,
                    RawData = response.Content
                        .ReadAsByteArrayAsync()
                        .Result
                };
            }

            return file;
        }

        /// <summary>
        /// Asynchronously retrieves raw data from a file by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>File information as a <see cref="FileResource"/> instance, or null if the file resource
        /// could not be found</returns>
        public async Task<FileResource> GetFileAsync(string apiUrl, params object[] apiUrlArgs)
        {
            var file = default(FileResource);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.GetAsync(url);
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                file = new FileResource()
                {
                    Name = response.Content.Headers.ContentDisposition.Name,
                    Size = response.Content.Headers.ContentLength ?? 0L,
                    ContentType = response.Content.Headers.ContentType.MediaType,
                    RawData = await response.Content.ReadAsByteArrayAsync()
                };
            }

            return file;
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
            var value = default(T);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.GetAsync(url).Result;
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<T>(response.Content.ReadAsStringAsync().Result);
            }

            return value;
        }

        /// <summary>
        /// Asynchronously retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        public async Task<T> GetAsync<T>(string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(T);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.GetAsync(url);
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<T>(await response.Content.ReadAsStringAsync());
            }

            return value;
        }

        /// <summary>
        /// Retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <typeparam name="TData">Type of request data to pass</typeparam>
        /// <param name="data">Additional data used by service request</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        public T Get<T, TData>(TData data, string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(T);
            var request = new HttpRequestMessage(HttpMethod.Get, GetApiResourceUrl(apiUrl, apiUrlArgs))
            {
                Content = new StringContent(JsonHelper.From(data, false), Encoding.UTF8, "application/json")
            };
            var response = _httpClient.SendAsync(request).Result;
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<T>(response.Content.ReadAsStringAsync().Result);
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
            LastResponse = GetResponse(response);
            return LastResponse;
        }

        /// <summary>
        /// Asynchronously inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public async Task<ServiceResponse> InsertAsync<T>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.PostAsJsonAsync(url, data);
            LastResponse = GetResponse(response);
            return LastResponse;
        }

        /// <summary>
        /// Inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public TValue Insert<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(TValue);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.PostAsJsonAsync(url, data).Result;
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<TValue>(response.Content.ReadAsStringAsync().Result);
            }

            return value;
        }

        /// <summary>
        /// Asynchronously inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public async Task<TValue> InsertAsync<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(TValue);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.PostAsJsonAsync(url, data);
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<TValue>(await response.Content.ReadAsStringAsync());
            }

            return value;
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
            LastResponse = GetResponse(response);
            return LastResponse;
        }

        /// <summary>
        /// Asynchronously updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public async Task<ServiceResponse> UpdateAsync<T>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.PutAsJsonAsync(url, data);
            LastResponse = GetResponse(response);
            return LastResponse;
        }

        /// <summary>
        /// Updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public TValue Update<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(TValue);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = _httpClient.PutAsJsonAsync(url, data).Result;
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<TValue>(response.Content.ReadAsStringAsync().Result);
            }

            return value;
        }

        /// <summary>
        /// Asynchronously updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public async Task<TValue> UpdateAsync<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs)
        {
            var value = default(TValue);
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.PutAsJsonAsync(url, data);
            LastResponse = GetResponse(response);
            if (LastResponse.Succeeded && response.StatusCode != HttpStatusCode.NotFound)
            {
                value = JsonHelper.To<TValue>(await response.Content.ReadAsStringAsync());
            }

            return value;
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
            LastResponse = GetResponse(response);
            return LastResponse;
        }

        /// <summary>
        /// Asynchronously deletes data by sending an HTTP DELETE request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        public async Task<ServiceResponse> DeleteAsync(string apiUrl, params object[] apiUrlArgs)
        {
            var url = GetApiResourceUrl(apiUrl, apiUrlArgs);
            var response = await _httpClient.DeleteAsync(url);
            LastResponse = GetResponse(response);
            return LastResponse;
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
                _httpClient?.Dispose();
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
                serviceResponse = new ServiceResponse(ServiceResult.ValidationFailed, message);
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var message = response.Content.ReadAsStringAsync().Result;
                serviceResponse = new ServiceResponse(ServiceResult.ServerError, message);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                serviceResponse = new ServiceResponse(ServiceResult.AccessDenied, "Access denied.");
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
        private string _serviceRoot;
        private bool _disposed = false;
    }
}
