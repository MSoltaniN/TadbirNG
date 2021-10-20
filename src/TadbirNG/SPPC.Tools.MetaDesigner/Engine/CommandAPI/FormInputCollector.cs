using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Framework.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class FormInputCollector : IUserInputCollector
    {
        public void GetInput()
        {
            if (InputForm == null)
            {
                throw ExceptionBuilder.NewInvalidOperationException("InputForm property is not set.");
            }

            if (InputForm is IMetadataEditor && InputForm.ShowDialog(Form.ActiveForm) == DialogResult.OK)
            {
                var editor = InputForm as IMetadataEditor;
                if (editor.HasMetadata)
                {
                    Output = editor.Metadata;
                }
            }
        }

        public object Output { get; private set; }
        public Form InputForm { get; set; }
    }
}
