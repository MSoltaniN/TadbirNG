using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SPPC.Framework.Service.Extensions
{
    /// <summary>
    /// Provides additional facilities for handling HTTP requests using the <see cref="HttpClient"/> class
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Asynchronously serializes given data as JSON and sends it to a URL using HTTP POST method
        /// </summary>
        /// <typeparam name="T">Type of data that must be sent</typeparam>
        /// <param name="httpClient">This object</param>
        /// <param name="url">Destination URL for the HTTP request that must be posted</param>
        /// <param name="data">Data to send to the destination URL</param>
        /// <returns>Asynchronous <see cref="Task"/> object for this operation</returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, Uri url, T data)
        {
            var content = GetJsonContent(data);
            return httpClient.PostAsync(url, content);
        }

        /// <summary>
        /// Asynchronously serializes given data as JSON and sends it to a URL using HTTP PUT method
        /// </summary>
        /// <typeparam name="T">Type of data that must be sent</typeparam>
        /// <param name="httpClient">This object</param>
        /// <param name="url">Destination URL for the HTTP request that must be sent</param>
        /// <param name="data">Data to send to the destination URL</param>
        /// <returns>Asynchronous <see cref="Task"/> object for this operation</returns>
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, Uri url, T data)
        {
            var content = GetJsonContent(data);
            return httpClient.PutAsync(url, content);
        }

        private static StringContent GetJsonContent<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
