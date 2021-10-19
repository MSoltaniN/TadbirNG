using System;
using System.Collections.Generic;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// Represents a key/value pair of string values
    /// </summary>
    public class KeyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValue"/> class.
        /// </summary>
        public KeyValue()
            : this(String.Empty, String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValue"/> class using specified key and value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the key part of this key/value pair.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value part of this key/value pair.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns a string representation for this object.
        /// </summary>
        /// <returns>String representation for this object</returns>
        public override string ToString()
        {
            return String.Format("[{0}] = '{1}'", Key, Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var keyValue = obj as KeyValue;
            if (keyValue == null)
            {
                return false;
            }

            return (keyValue.Key == Key && keyValue.Value == Value);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
