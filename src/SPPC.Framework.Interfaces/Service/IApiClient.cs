using System;
using System.Collections.Generic;

namespace SPPC.Framework.Service
{
    /// <summary>
    /// Defines common operations for working with a Web API (REST) service.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        T Get<T>(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        void Insert<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        void Update<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Deletes data by sending an HTTP DELETE request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        void Delete(string apiUrl, params object[] apiUrlArgs);
    }
}
