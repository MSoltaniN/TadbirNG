using System.IO;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Creates different types of storage metadata based on parameters given to factory methods.
    /// </summary>
    public class StorageFactory
    {
        /// <summary>
        /// Creates a new instance of Storage object for a file storage media.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Storage CreateFromFile(string path)
        {
            Verify.ArgumentNotNullOrWhitespace(path, nameof(path));
            var extension = Path.GetExtension(path);
            var media = extension == "xml"
                ? StorageMedia.XmlFile
                : StorageMedia.JsonFile;    // Database media is totally ignored
            return new Storage()
            {
                Name = Path.GetFileName(path),
                Connection = Path.GetDirectoryName(path),
                Media = media
            };
        }
    }
}
