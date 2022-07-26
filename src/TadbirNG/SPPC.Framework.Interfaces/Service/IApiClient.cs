using System.Threading.Tasks;
using SPPC.Framework.Values;

namespace SPPC.Framework.Service
{
    /// <summary>
    /// Defines common operations for working with a Web API (REST) service.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets or sets the URL for API service
        /// </summary>
        string ServiceRoot { get; set; }

        /// <summary>
        /// Gets the last response received from service
        /// </summary>
        ServiceResponse LastResponse { get; }

        /// <summary>
        /// Adds a single-valued HTTP header specified by name and value to all requests
        /// </summary>
        /// <param name="name">Name of header to add</param>
        /// <param name="value">Single value to set in added header</param>
        void AddHeader(string name, string value);

        /// <summary>
        /// Retrieves raw data from a file by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>File information as a <see cref="FileResource"/> instance, or null if the file resource
        /// could not be found</returns>
        FileResource GetFile(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously retrieves raw data from a file by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>File information as a <see cref="FileResource"/> instance, or null if the file resource
        /// could not be found</returns>
        Task<FileResource> GetFileAsync(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        T Get<T>(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        Task<T> GetAsync<T>(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Retrieves data by sending an HTTP GET request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve</typeparam>
        /// <typeparam name="TData">Type of request data to pass</typeparam>
        /// <param name="data">Additional data used by service request</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        /// <returns>Requested data deserialized from the API Service response</returns>
        T Get<T, TData>(TData data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        ServiceResponse Insert<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        Task<ServiceResponse> InsertAsync<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        TValue Insert<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously inserts data by sending an HTTP POST request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to insert</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to insert</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        Task<TValue> InsertAsync<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        ServiceResponse Update<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        Task<ServiceResponse> UpdateAsync<T>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        TValue Update<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously updates data by sending an HTTP PUT request to a Web API service.
        /// </summary>
        /// <typeparam name="T">Type of data to update</typeparam>
        /// <typeparam name="TValue">Type of data returned by API method</typeparam>
        /// <param name="data">Data to update</param>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        Task<TValue> UpdateAsync<T, TValue>(T data, string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Deletes data by sending an HTTP DELETE request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        ServiceResponse Delete(string apiUrl, params object[] apiUrlArgs);

        /// <summary>
        /// Asynchronously deletes data by sending an HTTP DELETE request to a Web API service.
        /// </summary>
        /// <param name="apiUrl">A URL value understandable by the underlying API controller</param>
        /// <param name="apiUrlArgs">Variable array of arguments required by the API URL</param>
        Task<ServiceResponse> DeleteAsync(string apiUrl, params object[] apiUrlArgs);
    }
}
