using System.Collections.Generic;
using SPPC.Framework.Values;

namespace SPPC.Framework.Service
{
    /// <summary>
    /// Provides information about the result of a request sent to the API service.
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse"/> class.
        /// </summary>
        /// <param name="result">A <see cref="ServiceResult"/> value that specifies the main result for this response.</param>
        /// <param name="message">A user-friendly text message that describes this response.</param>
        public ServiceResponse(
            ServiceResult result = ServiceResult.Success,
            string message = ValidationMessages.RequestSucceeded)
        {
            Result = result;
            Message = message;
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets a value that indicates if the operation corresponding to this response was successful.
        /// </summary>
        public bool Succeeded
        {
            get { return Result == ServiceResult.Success; }
        }

        /// <summary>
        /// Gets a message that explains this response.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets possible solutions that may prevent a failed operation.
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// Gets a value that shows the result of a service operation.
        /// </summary>
        public ServiceResult Result { get; }

        /// <summary>
        /// Gets a dictionary containing all headers returned in HTTP response
        /// </summary>
        /// <remarks>
        /// To simplify API, multiple values in a header are joined with pipe character (|)
        /// </remarks>
        public Dictionary<string, string> Headers { get; }
    }
}
