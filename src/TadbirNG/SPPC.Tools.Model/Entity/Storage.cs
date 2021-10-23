using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Represents a persistent data store.
    /// </summary>
    public class Storage
    {
        /// <summary>
        /// Gets or sets the media-specific name for this storage.
        /// </summary>
        /// <remarks>For an XML file storage, this member should contain XML file name (with file extension).
        /// For a database storage, this member should contain the logical name of the database.</remarks>
        [Description("Media-specific name for this storage.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media-specific connection information for this storage.
        /// </summary>
        /// <remarks>For an XML file storage, this member should contain the full directory path of XML file.
        /// For a database storage, this member should contain the provider-specific connection string.</remarks>
        [Description("Media-specific connection information for this storage.")]
        public string Connection { get; set; }

        /// <summary>
        /// Gets or sets the storage media type of this storage.
        /// </summary>
        [Description("Storage media type of this storage.")]
        public StorageMedia Media { get; set; }

        /// <summary>
        /// Provides a string representation for this storage.
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
