using System;
using System.Linq;
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

        private void NewWorkflow_Click(object sender, RoutedEventArgs e)
        {
            bool isNewConfirmed = HandleUnsavedChanges();
            if (isNewConfirmed)
            {
                ReloadDesignerControl();
                _ctlDesigner.NewWorkflow();
                _xamlPath = String.Empty;
            }
        }

        private void OpenWorkflow_Click(object sender, RoutedEventArgs e)
        {
            bool isOpenConfirmed = HandleUnsavedChanges();
            if (isOpenConfirmed)
            {
                var fileDialog = new OpenFileDialog()
                {
                    Title = "Select Workflow File",
                    Filter = "XAML Workflow Files (*.xaml)|*.xaml",
                    DefaultExt = "xaml"
                };

                if (fileDialog.ShowDialog(this) == true)
                {
                    _xamlPath = fileDialog.FileName;
                    ReloadDesignerControl();
                    _ctlDesigner.OpenWorkflow(_xamlPath);
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
            if (String.IsNullOrEmpty(_xamlPath))
            {
                HandleSaveAs();
            }
            else
            {
                _ctlDesigner.SaveWorkflow(_xamlPath);
            }
        }

        private bool HandleSaveAs()
        {
            bool isSaved = false;
            var fileDialog = new SaveFileDialog()
            {
                Title = "Save As",
                DefaultExt = "xaml",
                Filter = "XAML Workflow Files (*.xaml)|*.xaml",
                AddExtension = true
            };

            if (fileDialog.ShowDialog(this) == true)
            {
                _ctlDesigner.SaveWorkflow(fileDialog.FileName);
                _xamlPath = fileDialog.FileName;
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
