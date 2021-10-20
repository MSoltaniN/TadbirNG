using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class RepositoryView : IRepositoryView
    {
        public IRepositoryModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                if (value != null && _model != value)
                {
                    _model = value;
                    _model.ItemAdded += Repository_ItemAdded;
                    _model.ItemRemoved += Repository_ItemRemoved;
                }
            }
        }

        public ITreeViewNode CurrentNode
        {
            get { return null; }
        }

        public ITreeViewNode RootNode
        {
            get { return _rootNode; }
        }

        public void BuildTree()
        {
            var waitCursor = new WaitCursor();
            waitCursor.Set();
            _rootNode = Visualize(_model.Repository);
            waitCursor.Reset();
        }

        public void ExpandNodes(int maxLevel)
        {
            _rootNode.Expand();
            var flatNodes = FlattenNodes(_rootNode, maxLevel);
            Array.ForEach(flatNodes.ToArray(), node => node.Expand());
        }

        public event EventHandler<TreeNodeEventArgs> NodeAdded;
        public event EventHandler<TreeNodeEventArgs> NodeRemoved;

        private void Repository_ItemAdded(object sender, CollectionChangedEventArgs e)
        {
            var collectionNode = FindNode(e.Collection, _rootNode);
            if (collectionNode != null)
            {
                var nodeToAdd = Visualize(e.Item);
                collectionNode.AddChild(nodeToAdd);
                nodeToAdd.Select();
                RaiseNodeAddedEvent(nodeToAdd);
            }
        }

        private void Repository_ItemRemoved(object sender, CollectionChangedEventArgs e)
        {
            var nodeToRemove = FindNode(e.Item, _rootNode);
            var collectionNode = FindNode(e.Collection, _rootNode);
            if (nodeToRemove != null && collectionNode != null)
            {
                collectionNode.RemoveChild(nodeToRemove);
                collectionNode.Select();
                RaiseNodeRemovedEvent(nodeToRemove);
            }
        }

        private ITreeViewNode Visualize(object data)
        {
            var treeBuilder = new ObjectTreeBuilder();
            var tree = treeBuilder.BuildTree(data);
            return Transform(tree);
        }

        private ITreeViewNode Transform(Tree tree)
        {
            var rootNode = GetRootNode(tree);
            if (rootNode.Parent == null)
            {
                MetaDesignerContext.Current.Controller.SetNodeContextMenu(rootNode.Node as TreeNode);
            }

            foreach (var child in tree.Children)
            {
                var childNode = (IsCollection(child)) ? TransformCollection(child) : Transform(child);
                rootNode.AddChild(childNode);
                RaiseNodeAddedEvent(childNode);
            }

            return rootNode;
        }

        private bool IsCollection(Tree tree)
        {
            var metadata = tree.Metadata as ObjectMetadata;
            return (!String.IsNullOrEmpty(metadata.ItemType));
        }

        private ITreeViewNode TransformCollection(Tree collection)
        {
            var collectionNode = GetRootNode(collection);
            foreach (var item in collection.Children)
            {
                var childNode = Transform(item);
                collectionNode.AddChild(childNode);
                RaiseNodeAddedEvent(childNode);
            }

            return collectionNode;
        }

        private ITreeViewNode GetRootNode(Tree root)
        {
            Verify.ArgumentNotNull(root, "root");
            var metadata = root.Metadata as ObjectMetadata;
            var rootNode = new TreeViewNode();
            rootNode.SetNode(metadata.Name, metadata.Name, root.Data);
            return rootNode;
        }

        private ITreeViewNode FindNode(object tag, ITreeViewNode root)
        {
            var flatNodes = FlattenNodes(root);
            var nodeToFind = flatNodes.Where(node => Object.ReferenceEquals(node.Data, tag)).FirstOrDefault();
            return nodeToFind;
        }

        private IEnumerable<ITreeViewNode> FlattenNodes(ITreeViewNode root, int maxLevel = -1)
        {
            var flatItems = new List<ITreeViewNode>();
            if ((root.Level < maxLevel) || (maxLevel == -1))
            {
                flatItems.AddRange(root.Nodes);
                foreach (var node in root.Nodes)
                {
                    flatItems.AddRange(FlattenNodes(node, maxLevel));
                }
            }

            return flatItems;
        }

        private void RaiseNodeAddedEvent(ITreeViewNode addedNode)
        {
            NodeAdded?.Invoke(this, new TreeNodeEventArgs(addedNode));
        }

        private void RaiseNodeRemovedEvent(ITreeViewNode removedNode)
        {
            NodeRemoved?.Invoke(this, new TreeNodeEventArgs(removedNode));
        }

        private IRepositoryModel _model;
        private ITreeViewNode _rootNode;
    }
}
