using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Defines a generic hierarchical data structure that can be built from a source object.
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// Initializes a new instance of Tree class that is initially empty.
        /// </summary>
        public Tree()
        {
            _children = new List<Tree>();
            _leaves = new List<Leaf>();
        }

        /// <summary>
        /// Gets a Tree object that is immediately above this tree node in a tree hierarchy.
        /// </summary>
        public Tree Parent { get; private set; }

        /// <summary>
        /// Gets or sets the data item associated with the node represented by this Tree object.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets a custom metadata item that describes the node represented by this Tree object.
        /// </summary>
        public object Metadata { get; set; }

        /// <summary>
        /// Gets a collection of child tree nodes in the subtree represented by this Tree object.
        /// </summary>
        public IEnumerable<Tree> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets a collection of child leaf nodes in the subtree represented by this Tree object.
        /// </summary>
        public IEnumerable<Leaf> Leaves
        {
            get { return _leaves; }
        }

        /// <summary>
        /// Creates a flat sequence of tree items from the hierarchy of nodes under this tree.
        /// </summary>
        /// <returns>An enumerable sequence of tree items.</returns>
        public IEnumerable<Tree> Flatten()
        {
            var flatItems = new List<Tree>();
            flatItems.Add(this);
            foreach (var treeItem in Children)
            {
                flatItems.AddRange(treeItem.Flatten());
            }

            return flatItems;
        }

        /// <summary>
        /// Finds and returns the child tree item whose data is equal to the specified data object.
        /// </summary>
        /// <param name="childData">A data object to find in child tree items recursively.</param>
        /// <returns>A child tree object if specified data is assigned to a tree item under this tree;
        /// otherwise returns null.</returns>
        public Tree FindChild(object childData)
        {
            var flatItems = Flatten();
            var child = flatItems
                .Where(item => Object.ReferenceEquals(item.Data, childData))
                .FirstOrDefault();
            return child;
        }

        /// <summary>
        /// Adds a new child subtree to this Tree object.
        /// </summary>
        /// <param name="child">The child tree node to add.</param>
        public void AddChild(Tree child)
        {
            Verify.ArgumentNotNull(child, "child");
            child.Parent = this;
            _children.Add(child);
        }

        /// <summary>
        /// Inserts a new child subtree to this Tree object.
        /// </summary>
        /// <param name="child">The child tree node to insert.</param>
        public void InsertChild(int index, Tree child)
        {
            Verify.ArgumentNotNull(child, "child");
            child.Parent = this;
            _children.Insert(index, child);
        }

        /// <summary>
        /// Removes a child subtree from this Tree object.
        /// </summary>
        /// <param name="child">The child tree node to remove.</param>
        public void RemoveChild(Tree child)
        {
            Verify.ArgumentNotNull(child, "child");
            child.Parent = null;
            _children.Remove(child);
        }

        /// <summary>
        /// Adds a new child leaf node to this tree object.
        /// </summary>
        /// <param name="leaf">The child leaf node to add.</param>
        public void AddLeaf(Leaf leaf)
        {
            Verify.ArgumentNotNull(leaf, "leaf");
            leaf.Parent = this;
            _leaves.Add(leaf);
        }

        /// <summary>
        /// Provides a string representation for this tree node.
        /// </summary>
        /// <returns>String representation of this tree node.</returns>
        public override string ToString()
        {
            return ObjectNameProvider.GetName(Data);
        }

        private IList<Tree> _children;
        private IList<Leaf> _leaves;
    }
}
