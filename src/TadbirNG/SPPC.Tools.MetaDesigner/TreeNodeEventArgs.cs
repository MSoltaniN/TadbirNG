using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Common
{
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(ITreeViewNode node)
        {
            this.Node = node;
        }

        public ITreeViewNode Node { get; private set; }
    }
}
