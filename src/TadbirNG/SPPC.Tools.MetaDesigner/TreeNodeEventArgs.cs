using System;
using SPPC.Tools.Presentation;

namespace SPPC.Tools.MetaDesigner.Common
{
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(ITreeViewNode node)
        {
            Node = node;
        }

        public ITreeViewNode Node { get; private set; }
    }
}
