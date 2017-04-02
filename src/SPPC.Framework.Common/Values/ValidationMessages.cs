﻿using System;
using System.Collections.Generic;

namespace SPPC.Framework.Values
{
    /// <summary>
    /// Provides localized text for validation messages (in Persian).
    /// </summary>
    public sealed class ValidationMessages
    {
        private ValidationMessages()
        {
        }

        /// <summary>
        /// Localized text for a message indicating that operation was successfully completed.
        /// </summary>
        public const string RequestSucceeded = "درخواست مورد نظر با موفقیت انجام شد.";

        /// <summary>
        /// Localized text for a message indicating that a field is required.
        /// </summary>
        public const string FieldIsRequired = "وارد کردن {0} اجباری است.";

        /// <summary>
        /// Localized text for a message indicating that the value entered for a text field exceeds some length limit.
        /// </summary>
        public const string TextFieldIsTooLong = "{0} می تواند حداکثر {1} حرف داشته باشد.";

        /// <summary>
        /// Localized text for a message indicating that the length of a text field must fall within a specific range.
        /// </summary>
        public const string TextFieldHasLengthRange = "طول {0} باید بین {2} تا {1} حرف باشد.";

        /// <summary>
        /// Localized text for a validation message indicating that a field value is duplicate.
        /// </summary>
        public const string DuplicateFieldValue = "{0} تکراری است.";

        /// <summary>
        /// Localized text for a validation message indicating that two related field values are not equal.
        /// </summary>
        public const string FieldsDoNotMatch = "{0} و {1} نباید متفاوت باشند.";
    }
}
