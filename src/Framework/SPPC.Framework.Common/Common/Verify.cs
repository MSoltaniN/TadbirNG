using System;
using System.Collections.Generic;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Provides useful methods to perform argument checking in methods.
    /// </summary>
    /// <remarks>Use members of this class to check arguments in a method or property, before
    /// performing the main operations.
    /// </remarks>
    public static partial class Verify
    {
        /// <summary>
        /// Checks given argument for null reference. If argument is null, throws a new
        /// instance of ArgumentNullException.
        /// </summary>
        /// <param name="argValue">Argument to check for null reference</param>
        public static void ArgumentNotNull(object argValue)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException());
        }

        /// <summary>
        /// Checks given argument for null reference. If argument is null, throws a new
        /// instance of ArgumentNullException, with argument name of the generated exception
        /// set to argName.
        /// </summary>
        /// <param name="argValue">Argument to check for null reference</param>
        /// <param name="argName">Name of the argument to check</param>
        public static void ArgumentNotNull(object argValue, string argName)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException(argName));
        }

        /// <summary>
        /// Checks given string argument for null reference or empty value. If argument is null,
        /// throws a new instance of ArgumentNullException. If argument is empty string,
        /// throws a new instance of ArgumentException.
        /// </summary>
        /// <param name="argValue">String argument to check for null reference or empty value</param>
        public static void ArgumentNotNullOrEmptyString(string argValue)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException());
            if (String.IsNullOrEmpty(argValue))
                throw (ExceptionBuilder.NewArgumentException());
        }

        /// <summary>
        /// Checks given string argument for null reference or empty value. If argument is null,
        /// throws a new instance of ArgumentNullException. If argument is empty string,
        /// throws a new instance of ArgumentException. If an exception is generated, argument
        /// name of the exception instance is set to argName.
        /// </summary>
        /// <param name="argValue">String argument to check for null reference or empty value</param>
        /// <param name="argName">Name of the string argument to check</param>
        public static void ArgumentNotNullOrEmptyString(string argValue, string argName)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException(argName));
            if (String.IsNullOrEmpty(argValue))
                throw (ExceptionBuilder.NewArgumentException());
        }

        /// <summary>
        /// Checks given string argument for null reference or whitespace value. If argument is null,
        /// throws a new instance of ArgumentNullException. If argument is a string containing only
        /// whitespace characters, throws a new instance of ArgumentException.
        /// </summary>
        /// <param name="argValue">String argument to check for null reference or whitespace value</param>
        public static void ArgumentNotNullOrWhitespace(string argValue)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException());
            if (String.IsNullOrWhiteSpace(argValue))
                throw (ExceptionBuilder.NewArgumentException());
        }

        /// <summary>
        /// Checks given string argument for null reference or whitespace value. If argument is null,
        /// throws a new instance of ArgumentNullException. If argument is a string containing only
        /// whitespace characters, throws a new instance of ArgumentException. If an exception is generated,
        /// argument name of the exception instance is set to argName.
        /// </summary>
        /// <param name="argValue">String argument to check for null reference or whitespace value</param>
        /// <param name="argName">Name of the string argument to check</param>
        public static void ArgumentNotNullOrWhitespace(string argValue, string argName)
        {
            if (argValue == null)
                throw (ExceptionBuilder.NewArgumentNullException(argName));
            if (String.IsNullOrWhiteSpace(argValue))
                throw (ExceptionBuilder.NewArgumentException());
        }

        /// <summary>
        /// Checks given argument to make sure it is not less than a specified minimum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="minValue">The specified minimum value required by argument</param>
        public static void ArgumentNotLessThan(IComparable argValue, IComparable minValue)
        {
            VerifyNotNullAndIsAssignable(argValue, minValue);
            if (argValue.CompareTo(minValue) < 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException());
        }

        /// <summary>
        /// Checks given argument to make sure it is not less than a specified minimum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// If an exception is generated, argument name of the exception instance is set to argName.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="minValue">The specified minimum value required by argument</param>
        /// <param name="argName">Name of the argument to check</param>
        public static void ArgumentNotLessThan(IComparable argValue, IComparable minValue, string argName)
        {
            VerifyNotNullAndIsAssignable(argValue, minValue);
            if (argValue.CompareTo(minValue) < 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException(argName));
        }

        /// <summary>
        /// Checks given argument to make sure it is not greater than a specified maximum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="maxValue">The specified maximum value required by argument</param>
        public static void ArgumentNotGreaterThan(IComparable argValue, IComparable maxValue)
        {
            VerifyNotNullAndIsAssignable(argValue, maxValue);
            if (argValue.CompareTo(maxValue) > 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException());
        }

        /// <summary>
        /// Checks given argument to make sure it is not greater than a specified maximum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// If an exception is generated, argument name of the exception instance is set to argName.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="maxValue">The specified maximum value required by argument</param>
        /// <param name="argName">Name of the argument to check</param>
        public static void ArgumentNotGreaterThan(IComparable argValue, IComparable maxValue, string argName)
        {
            VerifyNotNullAndIsAssignable(argValue, maxValue);
            if (argValue.CompareTo(maxValue) > 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException(argName));
        }

        /// <summary>
        /// Checks given argument to make sure it is not outside a range specified by a minimum and a maximum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="minValue">The specified minimum value required by argument</param>
        /// <param name="maxValue">The specified maximum value required by argument</param>
        public static void ArgumentNotOutOfRange(IComparable argValue, IComparable minValue, IComparable maxValue)
        {
            VerifyNotNullAndIsAssignable(argValue, minValue, maxValue);
            if (argValue.CompareTo(minValue) < 0 || argValue.CompareTo(maxValue) > 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException());
        }

        /// <summary>
        /// Checks given argument to make sure it is not outside a range specified by a minimum and a maximum value.
        /// If the condition is not met, throws a new instance of ArgumentOutOfRangeException.
        /// If an exception is generated, argument name of the exception instance is set to argName.
        /// </summary>
        /// <param name="argValue">A comparable argument to check for required condition</param>
        /// <param name="minValue">The specified minimum value required by argument</param>
        /// <param name="maxValue">The specified maximum value required by argument</param>
        /// <param name="argName">Name of the argument to check</param>
        public static void ArgumentNotOutOfRange(IComparable argValue, IComparable minValue, IComparable maxValue,
            string argName)
        {
            VerifyNotNullAndIsAssignable(argValue, minValue, maxValue);
            if (argValue.CompareTo(minValue) < 0 || argValue.CompareTo(maxValue) > 0)
                throw (ExceptionBuilder.NewArgumentOutOfRangeException(argName));
        }

        /// <summary>
        /// Checks given integer argument to make sure it can be represented by the specified enum type.
        /// </summary>
        /// <param name="enumType">A Type that represents the enumeration class with the valid values.</param>
        /// <param name="argName">The name of the argument that must be checked.</param>
        /// <param name="argValue">The value of the argument that must be checked.</param>
        /// <remarks>InvalidEnumArgumentException type is not supported in Silverlight.</remarks>
        public static void EnumValueIsDefined(Type enumType, string argName, int argValue)
        {
            Verify.ArgumentNotNull(enumType, "enumType");

            // Make sure an enum type is supplied...
            if (!enumType.IsEnum)
                throw (ExceptionBuilder.NewArgumentException());
            if (!Enum.IsDefined(enumType, argValue))
            {
                throw (ExceptionBuilder.NewInvalidEnumArgumentException(argName, argValue, enumType));
            }
        }

        /// <summary>
        /// Checks to see if two given types can be assigned to each other.
        /// </summary>
        /// <param name="leftType">The Type to which a value needs to be assigned.</param>
        /// <param name="rightType">The Type whose value needs to be assigned.</param>
        public static void TypeIsAssignableFromType(Type leftType, Type rightType)
        {
            Verify.ArgumentNotNull(leftType, "leftType");

            if (!leftType.IsAssignableFrom(rightType))
                throw (ExceptionBuilder.NewArgumentException());
        }

        private static void VerifyNotNullAndIsAssignable(IComparable leftValue, IComparable rightValue)
        {
            Verify.ArgumentNotNull(leftValue, "leftValue");
            Verify.ArgumentNotNull(rightValue, "rightValue");

            Type leftType = leftValue.GetType();
            Type rightType = rightValue.GetType();
            Verify.TypeIsAssignableFromType(leftType, rightType);
        }

        private static void VerifyNotNullAndIsAssignable(IComparable argValue, IComparable minValue, IComparable maxValue)
        {
            Verify.ArgumentNotNull(argValue, "argValue");
            Verify.ArgumentNotNull(minValue, "minValue");
            Verify.ArgumentNotNull(maxValue, "maxValue");

            Type leftType = argValue.GetType();
            Type rightType = minValue.GetType();
            Verify.TypeIsAssignableFromType(leftType, rightType);

            rightType = maxValue.GetType();
            Verify.TypeIsAssignableFromType(leftType, rightType);
        }
    }
}
