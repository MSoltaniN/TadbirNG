using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SPPC.Framework.WorkflowManager
{
    /// <summary>
    /// Interaction logic for WorkflowDesignerControl.xaml
    /// </summary>
    public partial class WorkflowDesignerControl : UserControl
    {
        public WorkflowDesignerControl()
        {
            InitializeComponent();
            _isWorkflowDirty = false;
        }

        public void NewWorkflow()
        {
            _designer.Load(new Sequence());
        }

        public void OpenWorkflow(string path)
        {
            _designer.Load(path);
        }

        public void SaveWorkflow(string path)
        {
            _designer.Save(path);
            _isWorkflowDirty = false;
        }

        public bool HasUnsavedChanges()
        {
            return _isWorkflowDirty;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // register metadata
            (new DesignerMetadata()).Register();

            // create the workflow designer
            _designer = new WorkflowDesigner();
            DesignerBorder.Child = _designer.View;
            PropertyBorder.Child = _designer.PropertyInspectorView;
            _designer.ModelChanged += WorkflowDesigner_ModelChanged;
        }

        private void WorkflowDesigner_ModelChanged(object sender, EventArgs e)
        {
            _isWorkflowDirty = true;
        }

        private WorkflowDesigner _designer;
        private bool _isWorkflowDirty;
    }
}
