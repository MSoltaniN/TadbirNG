using System;
using System.Collections;
using System.Linq;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class RepositoryModel : IRepositoryModel
    {
        public RepositoryModel(Repository repository)
        {
            this.Repository = repository;
            var treeBuilder = new ObjectTreeBuilder();
            _repositoryTree = treeBuilder.BuildTree(repository);
        }

        public Repository Repository { get; private set; }

        public void AddItem(object item, Func<Repository, IEnumerable> collectionSelector)
        {
            var collectionTree = _repositoryTree.FindChild(collectionSelector(Repository));
            if (collectionTree != null)
            {
                var collection = collectionTree.Data as IList;
                collection.Add(item);
                var treeBuilder = new ObjectTreeBuilder();
                collectionTree.AddChild(treeBuilder.BuildTree(item));
                RaiseItemAddedEvent(item, collection);
            }
        }

        public void RemoveItem(object item, Func<Repository, IEnumerable> collectionSelector)
        {
            var collectionTree = _repositoryTree.FindChild(collectionSelector(Repository));
            if (collectionTree != null)
            {
                var collection = collectionTree.Data as IList;
                collection.Remove(item);
                collectionTree.RemoveChild(
                    collectionTree.Children.Where(child => Object.ReferenceEquals(child.Data, item)).First());
                RaiseItemRemovedEvent(item, collection);
            }
        }

        public event EventHandler<CollectionChangedEventArgs> ItemAdded;
        public event EventHandler<CollectionChangedEventArgs> ItemRemoved;

        private void RaiseItemAddedEvent(object item, IEnumerable collection)
        {
            ItemAdded?.Invoke(this, new CollectionChangedEventArgs(collection, item));
        }

        private void RaiseItemRemovedEvent(object item, IEnumerable collection)
        {
            ItemRemoved?.Invoke(this, new CollectionChangedEventArgs(collection, item));
        }

        private Tree _repositoryTree;
    }
}
