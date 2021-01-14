using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Persistence
{
    public class RepositoryStorageFactory
    {
        public static IRepositoryStorage GetStorage(Storage storage)
        {
            Verify.ArgumentNotNull(storage, "storage");
            if (storage.Media == StorageMedia.XmlFile)
            {
                return new XmlRepositoryStorage() { Storage = storage };
            }
            else
            {
                throw ExceptionBuilder.NewNotSupportedException();
            }
        }
    }
}
