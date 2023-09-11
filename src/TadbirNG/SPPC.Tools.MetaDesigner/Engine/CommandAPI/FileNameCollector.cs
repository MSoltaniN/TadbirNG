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
        {
            var filter = String.Join(
                '|', FileFilters.JsonRepository, FileFilters.XmlRepository, FileFilters.All);
            _fileDialog = open
                ? new OpenFileDialog() { Filter = filter }
                : new SaveFileDialog() { Filter = filter };
        }

        public void GetInput()
        {
            if (_fileDialog.ShowDialog(Form.ActiveForm) == DialogResult.OK)
            {
                Output = _fileDialog.FileName;
            }
        }

        public object Output { get; private set; }

        private readonly FileDialog _fileDialog;
    }
}
