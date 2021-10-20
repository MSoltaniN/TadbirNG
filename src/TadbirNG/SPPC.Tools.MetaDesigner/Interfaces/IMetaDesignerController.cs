using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface IMetaDesignerController
    {
        void Handle(object sender, EventArgs args);
        void SetNodeContextMenu(TreeNode node);
        IRepositoryView View { get; set; }
    }
}
