using System;
using SPPC.Tools.Presentation;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface IRepositoryView
    {
        IRepositoryModel Model { get; set; }

        ITreeViewNode RootNode { get; }

        ITreeViewNode CurrentNode { get; }

        void BuildTree();

        void ExpandNodes(int maxLevel);

        event EventHandler<TreeNodeEventArgs> NodeAdded;
        event EventHandler<TreeNodeEventArgs> NodeRemoved;
    }
}
