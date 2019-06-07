using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    public class PermissionCheckedEventArgs : EventArgs
    {
        public PermissionCheckedEventArgs(bool isChecked)
        {
            Checked = isChecked;
        }

        public bool Checked { get; }
    }
}
