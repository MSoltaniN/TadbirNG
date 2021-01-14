using System;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class WaitCursor : IWaitCursor
    {
        public void Set()
        {
            _activeForm = Form.ActiveForm;
            if (_activeForm != null)
            {
                _oldCursor = _activeForm.Cursor;
                _activeForm.Cursor = Cursors.WaitCursor;
            }
        }

        public void Reset()
        {
            if (_activeForm != null)
            {
                _activeForm.Cursor = _oldCursor;
            }
        }

        private Cursor _oldCursor;
        private Form _activeForm;
    }
}
