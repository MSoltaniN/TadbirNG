using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class TreeViewNode : ITreeViewNode
    {
        public object Node
        {
            get { return _node; }
            set { _node = value as TreeNode; }
        }

        public string Name
        {
            get { return _node.Name; }
            set { _node.Name = value; }
        }

        public string Text
        {
            get { return _node.Text; }
            set { _node.Text = value; }
        }

        public object Data
        {
            get { return _node.Tag; }
            set { _node.Tag = value; }
        }

        public int Level
        {
            get { return _node.Level; }
        }

        public ITreeViewNode Parent { get; private set; }
        public IEnumerable<ITreeViewNode> Nodes
        {
            get { return _nodes; }
        }

        public void SetParent(ITreeViewNode parent)
        {
            Parent = parent;
        }

        public void SetNode(string name, string text, object data)
        {
            _node = new TreeNode()
            {
                Name = name,
                Text = text,
                Tag = data
            };
        }

        public void AddChild(ITreeViewNode child)
        {
            if (child.Node is TreeNode childNode)
            {
                _node.Nodes.Add(childNode);
                child.SetParent(this);
                _nodes.Add(child);
            }
        }

        public void InsertChild(int index, ITreeViewNode child)
        {
            if (child.Node is TreeNode childNode)
            {
                _node.Nodes.Insert(index, childNode);
                child.SetParent(this);
                _nodes.Insert(index, child);
            }
        }

        public void RemoveChild(ITreeViewNode child)
        {
            var childNode = child.Node as TreeNode;
            if (childNode != null)
            {
                childNode.Remove();
                _nodes.Remove(child);
            }
        }

        public void Expand()
        {
            _node.Expand();
        }

        public void Select()
        {
            _node.TreeView.SelectedNode = _node;
        }

        private TreeNode _node;
        private readonly List<ITreeViewNode> _nodes = new();
    }
}
