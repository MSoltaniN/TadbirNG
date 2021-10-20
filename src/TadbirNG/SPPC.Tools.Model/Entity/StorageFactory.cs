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
        /// Creates a new instance of Storage object for an XML file data storage media.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Storage CreateFile(string path)
        {
            Verify.ArgumentNotNullOrWhitespace(path, "path");
            return new Storage()
            {
                Name = Path.GetFileName(path),
                Connection = Path.GetDirectoryName(path),
                Media = StorageMedia.XmlFile
            };
        }
    }
}
