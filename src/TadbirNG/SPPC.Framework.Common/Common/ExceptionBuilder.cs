using System;
using System.ComponentModel;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Initializes and returns standard exceptions.
    /// </summary>
    /// <remarks>Wherever possible, use methods of this class to build exceptions, instead of directly creating them.
    /// </remarks>
    public static partial class ExceptionBuilder
    {
        #region ArgumentException builder

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentException class.
        /// </summary>
        /// <returns>A new default instance of ArgumentException class.</returns>
        public static ArgumentException NewArgumentException()
        {
            return (new ArgumentException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of ArgumentException class with specified arguments.</returns>
        public static ArgumentException NewArgumentException(string message)
        {
            return (new ArgumentException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentException class with
        /// a specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns>A new instance of ArgumentException class with specified arguments.</returns>
        public static ArgumentException NewArgumentException(string message, Exception innerException)
        {
            return (new ArgumentException(message, innerException));
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentException class with
        /// a specified error message and the name of the parameter that causes this
        /// exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the
        /// exception.</param>
        /// <param name="parameterName">The name of the parameter that caused the current
        /// exception.</param>
        /// <returns>A new instance of ArgumentException class with specified arguments.</returns>
        public static ArgumentException NewArgumentException(string message, string parameterName)
        {
            return (new ArgumentException(message, parameterName));
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentException class with
        /// a specified error message, the parameter name and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the
        /// exception.</param>
        /// <param name="parameterName">The name of the parameter that caused the current
        /// exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns></returns>
        public static ArgumentException NewArgumentException(string message, string parameterName,
            Exception innerException)
        {
            return (new ArgumentException(message, parameterName, innerException));
        }

        #endregion

        #region ArgumentNullException builder

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentNullException class.
        /// </summary>
        /// <returns>A new default instance of ArgumentNullException class.</returns>
        public static ArgumentNullException NewArgumentNullException()
        {
            return (new ArgumentNullException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentNullException class with
        /// the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <returns>A new instance of ArgumentNullException class with specified arguments.</returns>
        public static ArgumentNullException NewArgumentNullException(string parameterName)
        {
            return (new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentNullException class with
        /// a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this
        /// exception.</param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception, or a null reference if no inner exception is specified.</param>
        /// <returns>A new instance of ArgumentNullException class with specified arguments.</returns>
        public static ArgumentNullException NewArgumentNullException(string message, Exception innerException)
        {
            return (new ArgumentNullException(message, innerException));
        }

        /// <summary>
        /// Initializes and returns an instance of the ArgumentNullException class with
        /// a specified error message and the name of the parameter that causes this
        /// exception.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <returns>A new instance of ArgumentNullException class with specified arguments.</returns>
        public static ArgumentNullException NewArgumentNullException(string parameterName, string message)
        {
            return (new ArgumentNullException(parameterName, message));
        }

        #endregion

        #region ArgumentOutOfRangeException builder

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentOutOfRangeException class.
        /// </summary>
        /// <returns>A new default instance of ArgumentOutOfRangeException class.</returns>
        public static ArgumentOutOfRangeException NewArgumentOutOfRangeException()
        {
            return (new ArgumentOutOfRangeException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentOutOfRangeException class with
        /// the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <returns>A new instance of ArgumentOutOfRangeException class with specified arguments.</returns>
        public static ArgumentOutOfRangeException NewArgumentOutOfRangeException(string parameterName)
        {
            return (new ArgumentOutOfRangeException(parameterName));
        }

        /// <summary>
        /// Initializes and returns a new instance of the ArgumentOutOfRangeException class with
        /// a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this
        /// exception.</param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception, or a null reference if no inner exception is specified.</param>
        /// <returns>A new instance of ArgumentOutOfRangeException class with specified arguments.</returns>
        public static ArgumentOutOfRangeException NewArgumentOutOfRangeException(string message, Exception innerException)
        {
            return (new ArgumentOutOfRangeException(message, innerException));
        }

        /// <summary>
        /// Initializes and returns an instance of the ArgumentOutOfRangeException class with
        /// a specified error message and the name of the parameter that causes this
        /// exception.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <returns>A new instance of ArgumentOutOfRangeException class with specified arguments.</returns>
        public static ArgumentOutOfRangeException NewArgumentOutOfRangeException(string parameterName, string message)
        {
            return (new ArgumentOutOfRangeException(parameterName, message));
        }

        /// <summary>
        /// Initializes and returns an instance of the ArgumentOutOfRangeException class with
        /// a specified error message, the value of the argument and the name of the parameter that causes this
        /// exception.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="actualValue">The value of the argument that caused this exception</param>
        /// <param name="message">A message that describes the error.</param>
        /// <returns></returns>
        public static ArgumentOutOfRangeException NewArgumentOutOfRangeException(string parameterName, object actualValue,
            string message)
        {
            return (new ArgumentOutOfRangeException(parameterName, actualValue, message));
        }

        #endregion

        #region InvalidEnumArgumentException builder

        /// <summary>
        /// Initializes and returns a new instance of the InvalidEnumArgumentException class without a message.
        /// </summary>
        /// <returns>A new default instance of InvalidEnumArgumentException class.</returns>
        public static InvalidEnumArgumentException NewInvalidEnumArgumentException()
        {
            return (new InvalidEnumArgumentException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the InvalidEnumArgumentException class with the specified message.
        /// </summary>
        /// <param name="message">The message to display with this exception.</param>
        /// <returns>A new instance of InvalidEnumArgumentException class with specified arguments.</returns>
        public static InvalidEnumArgumentException NewInvalidEnumArgumentException(string message)
        {
            return (new InvalidEnumArgumentException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the InvalidEnumArgumentException class with the specified
        /// detailed description and the specified exception.
        /// </summary>
        /// <param name="message">The message to display with this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception. If the innerException parameter is not a null reference, the current
        /// exception is raised in a catch block that handles the inner exception.</param>
        /// <returns>A new instance of InvalidEnumArgumentException class with specified arguments.</returns>
        public static InvalidEnumArgumentException NewInvalidEnumArgumentException(string message, Exception innerException)
        {
            return (new InvalidEnumArgumentException(message, innerException));
        }

        /// <summary>
        /// Initializes and returns a new instance of the InvalidEnumArgumentException class with a message generated
        /// from the argument, the invalid value, and an enumeration class.
        /// </summary>
        /// <param name="argName">The name of the parameter that caused the current exception.</param>
        /// <param name="invalidValue">The value of the argument that failed.</param>
        /// <param name="enumClass">A Type that represents the enumeration class with the valid values.</param>
        /// <returns>A new instance of InvalidEnumArgumentException class with specified arguments.</returns>
        public static InvalidEnumArgumentException NewInvalidEnumArgumentException(string argName, int invalidValue,
            Type enumClass)
        {
            return (new InvalidEnumArgumentException(argName, invalidValue, enumClass));
        }

        #endregion

        #region InvalidOperationException builder

        /// <summary>
        /// Initializes and returns a new instance of the InvalidOperationException class.
        /// </summary>
        /// <returns>A new default instance of InvalidOperationException class.</returns>
        public static InvalidOperationException NewInvalidOperationException()
        {
            return (new InvalidOperationException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the InvalidOperationException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of InvalidOperationException class with specified arguments.</returns>
        public static InvalidOperationException NewInvalidOperationException(string message)
        {
            return (new InvalidOperationException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the InvalidOperationException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns>A new instance of InvalidOperationException class with specified arguments.</returns>
        public static InvalidOperationException NewInvalidOperationException(string message,
            Exception innerException)
        {
            return (new InvalidOperationException(message, innerException));
        }

        #endregion

        #region NotSupportedException builder

        /// <summary>
        /// Initializes and returns a new instance of the NotSupportedException class.
        /// </summary>
        /// <returns>A new default instance of NotSupportedException class.</returns>
        public static NotSupportedException NewNotSupportedException()
        {
            return (new NotSupportedException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the NotSupportedException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of NotSupportedException class with specified arguments.</returns>
        public static NotSupportedException NewNotSupportedException(string message)
        {
            return (new NotSupportedException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the NotSupportedException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns>A new instance of NotSupportedException class with specified arguments.</returns>
        public static NotSupportedException NewNotSupportedException(string message, Exception innerException)
        {
            return (new NotSupportedException(message, innerException));
        }

        #endregion

        #region NotImplementedException builder

        /// <summary>
        /// Initializes and returns a new instance of the NotImplementedException class.
        /// </summary>
        /// <returns>A new default instance of NotImplementedException class.</returns>
        public static NotImplementedException NewNotImplementedException()
        {
            return (new NotImplementedException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the NotImplementedException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of NotImplementedException class with specified arguments.</returns>
        public static NotImplementedException NewNotImplementedException(string message)
        {
            return (new NotImplementedException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the NotImplementedException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns>A new instance of NotImplementedException class with specified arguments.</returns>
        public static NotImplementedException NewNotImplementedException(string message, Exception innerException)
        {
            return (new NotImplementedException(message, innerException));
        }

        #endregion

        #region FormatException builder

        /// <summary>
        /// Initializes and returns a new instance of the FormatException class.
        /// </summary>
        /// <returns>A new default instance of FormatException class.</returns>
        public static FormatException NewFormatException()
        {
            return (new FormatException());
        }

        /// <summary>
        /// Initializes and returns a new instance of the FormatException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of FormatException class with specified arguments.</returns>
        public static FormatException NewFormatException(string message)
        {
            return (new FormatException(message));
        }

        /// <summary>
        /// Initializes and returns a new instance of the FormatException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        /// <returns>A new instance of FormatException class with specified arguments.</returns>
        public static FormatException NewFormatException(string message, Exception innerException)
        {
            return (new FormatException(message, innerException));
        }

        #endregion

        #region Generic exception builder

        /// <summary>
        /// Initializes and returns a new instance of a generic exception class.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to build</typeparam>
        /// <returns>A new default instance of the specified exception class.</returns>
        public static TException NewGenericException<TException>()
            where TException : Exception, new()
        {
            return new TException();
        }

        /// <summary>
        /// Initializes and returns a new instance of a generic exception class with a specified error message.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to build</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new instance of the specified exception class with specified arguments.</returns>
        public static TException NewGenericException<TException>(string message)
            where TException : Exception, new()
        {
            return (Reflector.Instantiate(typeof(TException), message) as TException);
        }

        /// <summary>
        /// Initializes and returns a new instance of a generic exception class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to build</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.</param>
        /// <returns>A new instance of the specified exception class with specified arguments.</returns>
        public static TException NewGenericException<TException>(string message, Exception innerException)
            where TException : Exception, new()
        {
            return (Reflector.Instantiate(typeof(TException), message, innerException) as TException);
        }

        #endregion
    }
}
