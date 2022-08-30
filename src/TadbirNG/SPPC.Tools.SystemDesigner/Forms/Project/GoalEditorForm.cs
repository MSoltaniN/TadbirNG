using System;
using System.Windows.Forms;

namespace SPPC.Tools.SystemDesigner
{
    public partial class GoalEditorForm : UserControl, IEditorForm
    {
        public GoalEditorForm()
        {
            InitializeComponent();
        }

        public object Entity { get; set; }

        public event EventHandler<EventArgs> Saved;

        public event EventHandler<EventArgs> Cancelled;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EditorDriver.SetupBindings(this, Entity);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var result = EditorDriver.GetValidationResult(Entity);
            if (!String.IsNullOrEmpty(result))
            {
                MessageBox.Show(Form.ActiveForm, result, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RaiseSavedEvent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            RaiseCancelledEvent();
        }

        private void RaiseSavedEvent()
        {

            Saved?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseCancelledEvent()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
