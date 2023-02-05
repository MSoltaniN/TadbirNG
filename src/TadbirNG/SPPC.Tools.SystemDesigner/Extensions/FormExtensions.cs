using System.Windows.Forms;

namespace SPPC.Tools.Extensions
{
    public static class FormExtensions
    {
        public static Form GetActiveForm(this Form form)
        {
            return Form.ActiveForm ?? form;
        }
    }
}
