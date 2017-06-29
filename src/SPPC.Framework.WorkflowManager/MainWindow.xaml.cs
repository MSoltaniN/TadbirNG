using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SPPC.Framework.WorkflowManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string WorkflowPath
        {
            get
            {
                return _xamlPath;
            }

            set
            {
                _xamlPath = value;
                AddToWindowTitle(_xamlPath);
            }
        }

        private void NewWorkflow_Click(object sender, RoutedEventArgs e)
        {
            bool isNewConfirmed = HandleUnsavedChanges();
            if (isNewConfirmed)
            {
                ReloadDesignerControl();
                _ctlDesigner.NewWorkflow();
                WorkflowPath = String.Empty;
            }
        }

        private void OpenWorkflow_Click(object sender, RoutedEventArgs e)
        {
            bool isOpenConfirmed = HandleUnsavedChanges();
            if (isOpenConfirmed)
            {
                var fileDialog = new OpenFileDialog()
                {
                    Title = Strings.OpenWorkflowTitle,
                    Filter = Strings.WorkflowFileFilter,
                    DefaultExt = Strings.DefaultWorkflowExtension
                };

                if (fileDialog.ShowDialog(this) == true)
                {
                    WorkflowPath = fileDialog.FileName;
                    ReloadDesignerControl();
                    _ctlDesigner.OpenWorkflow(WorkflowPath);
                }
            }
        }

        private void SaveWorkflow_Click(object sender, RoutedEventArgs e)
        {
            HandleSave();
        }

        private void SaveWorkflowAs_Click(object sender, RoutedEventArgs e)
        {
            HandleSaveAs();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddToWindowTitle(string xamlPath)
        {
            string title = Strings.ApplicationTitle;
            if (!String.IsNullOrEmpty(xamlPath))
            {
                title = String.Format("{0} - {1}", title, Path.GetFileName(xamlPath));
            }

            this.Title = title;
        }

        private void ReloadDesignerControl()
        {
            if (_ctlDesigner != null)
            {
                grdMain.Children.Remove(_ctlDesigner);
            }

            _ctlDesigner = new WorkflowDesignerControl()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Grid.SetRow(_ctlDesigner, 1);
            grdMain.Children.Add(_ctlDesigner);
        }

        private void HandleSave()
        {
            if (String.IsNullOrEmpty(WorkflowPath))
            {
                HandleSaveAs();
            }
            else
            {
                _ctlDesigner.SaveWorkflow(WorkflowPath);
            }
        }

        private bool HandleSaveAs()
        {
            bool isSaved = false;
            var fileDialog = new SaveFileDialog()
            {
                Title = Strings.SaveWorkflowTitle,
                DefaultExt = Strings.DefaultWorkflowExtension,
                Filter = Strings.WorkflowFileFilter,
                AddExtension = true
            };

            if (fileDialog.ShowDialog(this) == true)
            {
                _ctlDesigner.SaveWorkflow(fileDialog.FileName);
                WorkflowPath = fileDialog.FileName;
                isSaved = true;
            }

            return isSaved;
        }

        private bool HandleUnsavedChanges()
        {
            bool isConfirmed = true;
            if (_ctlDesigner != null && _ctlDesigner.HasUnsavedChanges())
            {
                var result = MessageBox.Show(this,
                    "There are changes in current workflow that are not saved.\r\nWould you like to save them first?",
                    "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    HandleSave();
                }
                else
                {
                    isConfirmed = (result == MessageBoxResult.No);
                }
            }

            return isConfirmed;
        }

        private string _xamlPath;
        private WorkflowDesignerControl _ctlDesigner;
    }
}
