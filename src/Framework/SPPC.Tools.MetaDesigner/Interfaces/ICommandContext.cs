using System;
using System.Collections;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface ICommandContext
    {
        string Command { get; set; }
        string Title { get; }
        IRepositoryModel Model { get; set; }
        object Item { get; set; }
        IList Collection { get; set; }
        object Object { get; set; }
        bool IsItemContext { get; }
        bool IsCollectionContext { get; }
        bool IsObjectContext { get; }
    }
}
