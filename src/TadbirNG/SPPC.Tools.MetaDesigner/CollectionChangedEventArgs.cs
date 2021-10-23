using System;
using System.Collections;

namespace SPPC.Tools.MetaDesigner.Common
{
    public class CollectionChangedEventArgs : EventArgs
    {
        public CollectionChangedEventArgs(IEnumerable collection, object item)
        {
            this.Item = item;
            this.Collection = collection;
        }

        public object Item { get; private set; }
        public IEnumerable Collection { get; private set; }
    }
}
