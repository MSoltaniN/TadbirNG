using System;
using System.Collections;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface IRepositoryModel
    {
        Repository Repository { get; }

        void AddItem(object item, Func<Repository, IEnumerable> collectionSelector);
        void RemoveItem(object item, Func<Repository, IEnumerable> collectionSelector);

        event EventHandler<CollectionChangedEventArgs> ItemAdded;
        event EventHandler<CollectionChangedEventArgs> ItemRemoved;
    }
}
