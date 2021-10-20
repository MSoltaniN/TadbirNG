using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class FileNameCollector : IUserInputCollector
    {
        public FileNameCollector(bool open)
            : this(open, FileFilters.All)
        {
        }

        public FileNameCollector(bool open, string filter)
        {
            _fileDialog = open
                ? new OpenFileDialog() { Filter = filter } as FileDialog
                : new SaveFileDialog() { Filter = filter } as FileDialog;
        }

        public void GetInput()
        {
            if (_fileDialog.ShowDialog(Form.ActiveForm) == DialogResult.OK)
            {
                Output = _fileDialog.FileName;
            }
        }

        public object Output { get; private set; }

        private FileDialog _fileDialog;
    }
}
