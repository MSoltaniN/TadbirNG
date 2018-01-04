using System;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Runtime.Versioning;
using System.ServiceModel.Activities;
using System.Windows.Controls;
using Microsoft.CSharp.Activities;
using SPPC.Workflow.Design.Internal;

namespace SPPC.Workflow.Design
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

            ToolboxBorder.Child = GetToolboxControl();
            DesignerBorder.Child = _designer.View;
            PropertyBorder.Child = _designer.PropertyInspectorView;
            _designer.ModelChanged += WorkflowDesigner_ModelChanged;
        }

        private static ToolboxControl GetToolboxControl()
        {
            var toolbox = new ToolboxControl();
            var category = new ToolboxCategory("Control Flow");
            category.Add(
                new ToolboxItemWrapper(typeof(DoWhile).FullName, typeof(DoWhile).Assembly.FullName, null, "DoWhile"));
            category.Add(
                new ToolboxItemWrapper(typeof(ForEach<>).FullName, typeof(ForEach<>).Assembly.FullName, null, "ForEach<T>"));
            category.Add(
                new ToolboxItemWrapper(typeof(If).FullName, typeof(If).Assembly.FullName, null, "If"));
            category.Add(
                new ToolboxItemWrapper(typeof(Parallel).FullName, typeof(Parallel).Assembly.FullName, null, "Parallel"));
            category.Add(
                new ToolboxItemWrapper(typeof(ParallelForEach<>).FullName, typeof(ParallelForEach<>).Assembly.FullName, null, "ParallelForEach<T>"));
            category.Add(
                new ToolboxItemWrapper(typeof(Pick).FullName, typeof(Pick).Assembly.FullName, null, "Pick"));
            category.Add(
                new ToolboxItemWrapper(typeof(PickBranch).FullName, typeof(PickBranch).Assembly.FullName, null, "PickBranch"));
            category.Add(
                new ToolboxItemWrapper(typeof(Sequence).FullName, typeof(Sequence).Assembly.FullName, null, "Sequence"));
            category.Add(
                new ToolboxItemWrapper(typeof(Switch<>).FullName, typeof(Switch<>).Assembly.FullName, null, "Switch<T>"));
            category.Add(
                new ToolboxItemWrapper(typeof(While).FullName, typeof(While).Assembly.FullName, null, "While"));
            toolbox.Categories.Add(category);

            category = new ToolboxCategory("Flowchart");
            category.Add(
                new ToolboxItemWrapper(typeof(Flowchart).FullName, typeof(Flowchart).Assembly.FullName, null, "Flowchart"));
            category.Add(
                new ToolboxItemWrapper(typeof(FlowDecision).FullName, typeof(FlowDecision).Assembly.FullName, null, "FlowDecision"));
            category.Add(
                new ToolboxItemWrapper(typeof(FlowSwitch<>).FullName, typeof(FlowSwitch<>).Assembly.FullName, null, "FlowSwitch<T>"));
            toolbox.Categories.Add(category);

            category = new ToolboxCategory("State Machine");
            category.Add(
                new ToolboxItemWrapper(typeof(StateMachine).FullName, typeof(StateMachine).Assembly.FullName, null, "StateMachine"));
            category.Add(
                new ToolboxItemWrapper(typeof(State).FullName, typeof(State).Assembly.FullName, null, "State"));
            category.Add(
                new ToolboxItemWrapper(typeof(State).FullName, typeof(State).Assembly.FullName, null, "FinalState"));
            toolbox.Categories.Add(category);

            category = new ToolboxCategory("Messaging");
            category.Add(
                new ToolboxItemWrapper(typeof(CorrelationScope).FullName, typeof(CorrelationScope).Assembly.FullName, null, "CorrelationScope"));
            category.Add(
                new ToolboxItemWrapper(typeof(InitializeCorrelation).FullName, typeof(InitializeCorrelation).Assembly.FullName, null, "InitializeCorrelation"));
            category.Add(
                new ToolboxItemWrapper(typeof(Receive).FullName, typeof(Receive).Assembly.FullName, null, "Receive"));
            category.Add(
                new ToolboxItemWrapper(typeof(ReceiveReply).FullName, typeof(ReceiveReply).Assembly.FullName, null, "ReceiveReply"));
            category.Add(
                new ToolboxItemWrapper(typeof(Send).FullName, typeof(Send).Assembly.FullName, null, "Send"));
            category.Add(
                new ToolboxItemWrapper(typeof(SendReply).FullName, typeof(SendReply).Assembly.FullName, null, "SendReply"));
            category.Add(
                new ToolboxItemWrapper(typeof(TransactedReceiveScope).FullName, typeof(TransactedReceiveScope).Assembly.FullName, null, "TransactedReceiveScope"));
            toolbox.Categories.Add(category);

            category = new ToolboxCategory("Primitives");
            category.Add(
                new ToolboxItemWrapper(typeof(Assign).FullName, typeof(Assign).Assembly.FullName, null, "Assign"));
            category.Add(
                new ToolboxItemWrapper(typeof(Delay).FullName, typeof(Delay).Assembly.FullName, null, "Delay"));
            category.Add(
                new ToolboxItemWrapper(typeof(InvokeDelegate).FullName, typeof(InvokeDelegate).Assembly.FullName, null, "InvokeDelegate"));
            category.Add(
                new ToolboxItemWrapper(typeof(InvokeMethod).FullName, typeof(InvokeMethod).Assembly.FullName, null, "InvokeMethod"));
            category.Add(
                new ToolboxItemWrapper(typeof(WriteLine).FullName, typeof(WriteLine).Assembly.FullName, null, "WriteLine"));
            toolbox.Categories.Add(category);
            return toolbox;
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
