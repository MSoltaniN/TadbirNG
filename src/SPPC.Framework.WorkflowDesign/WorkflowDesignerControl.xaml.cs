using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Controls;
using Microsoft.CSharp.Activities;
using RehostedWorkflowDesigner.CSharpExpressionEditor;

namespace SPPC.Framework.WorkflowDesign
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
            _designer.Load("NewWorkflow.xaml");
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

            // Register Custom C# expression editor...
            _editorService = new RoslynExpressionEditorService();
            ExpressionTextBox.RegisterExpressionActivityEditor(
                new CSharpValue<string>().Language, typeof(RoslynExpressionEditor),
                CSharpExpressionHelper.CreateExpressionFromString);

            _designer = new WorkflowDesigner();

            // Disable the default VB expression editor...
            DesignerConfigurationService configurationService =
                _designer.Context.Services.GetService<DesignerConfigurationService>();
            configurationService.TargetFrameworkName = new FrameworkName(".NETFramework", new Version(4, 5));
            configurationService.LoadingFromUntrustedSourceEnabled = true;
            _designer.Context.Services.Publish<IExpressionEditorService>(_editorService);

            // Associate all of the basic activities with their designers
            (new DesignerMetadata()).Register();

            DesignerBorder.Child = _designer.View;
            PropertyBorder.Child = _designer.PropertyInspectorView;
            _designer.ModelChanged += WorkflowDesigner_ModelChanged;
        }

        private void WorkflowDesigner_ModelChanged(object sender, EventArgs e)
        {
            _isWorkflowDirty = true;
        }

        private WorkflowDesigner _designer;
        private RoslynExpressionEditorService _editorService;
        private bool _isWorkflowDirty;
    }
}
