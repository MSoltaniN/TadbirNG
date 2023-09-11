using System.Windows.Forms;
using SPPC.Framework.Common;

namespace SPPC.Tools.Presentation
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
