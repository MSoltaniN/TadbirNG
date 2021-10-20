using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface ITreeViewNode
    {
        object Node { get; set; }
        string Name { get; set; }
        string Text { get; set; }
        object Data { get; set; }
        int Level { get; }
        ITreeViewNode Parent { get; }
        IEnumerable<ITreeViewNode> Nodes { get; }

        void SetNode(string name, string text, object data);
        void SetParent(ITreeViewNode parent);
        void AddChild(ITreeViewNode child);
        void RemoveChild(ITreeViewNode child);
        void Expand();
        void Select();
    }
}
