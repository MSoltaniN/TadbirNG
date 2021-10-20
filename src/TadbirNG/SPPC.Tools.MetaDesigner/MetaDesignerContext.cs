using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tools.MetaDesigner.Engine;

namespace SPPC.Tools.MetaDesigner.Common
{
    public class MetaDesignerContext : IMetaDesignerContextManager
    {
        public MetaDesignerContext()
        {
            View = new RepositoryView();
            Controller = new CommandHandler
            {
                View = View
            };
        }

        public static MetaDesignerContext Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new MetaDesignerContext();
                }

                return _current;
            }
        }

        public IRepositoryModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                if (value != null && value != _model)
                {
                    _model = value;
                    View.Model = _model;
                    View.BuildTree();
                    View.ExpandNodes(1);
                }
            }
        }

        public IRepositoryView View { get; private set; }
        public IMetaDesignerController Controller { get; private set; }

        private static MetaDesignerContext _current;
        private IRepositoryModel _model;
    }
}
