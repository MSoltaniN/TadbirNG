using BabakSoft.Platform.Data;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class ViewWizardForm : Form
    {
        public ViewWizardForm()
        {
            InitializeComponent();
            WizardModel = new ViewModelWizard();
        }

        public ViewModelWizard WizardModel { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFirstPage();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (_currentStepNo == 2)
            {
                _currentStepNo--;
                LoadFirstPage();
            }
            else if (_currentStepNo == 3)
            {
                _currentStepNo--;
                LoadSecondPage();
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (_currentStepNo == 1)
            {
                _currentStepNo++;
                LoadSecondPage();
            }
            else if (_currentStepNo == 2)
            {
                _currentStepNo++;
                LoadThirdPage();
            }
            else
            {
                GenerateScript();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LoadFirstPage()
        {
            var page = new SysViewMoldelsForm() { Dock = DockStyle.Fill, ViewModelWizard = WizardModel };
            splitContainerNested.Panel1.Controls.Clear();
            splitContainerNested.Panel1.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Add New ViewModel";
        }

        private void LoadSecondPage()
        {
            var page = new SelectViewModelForm () { Dock = DockStyle.Fill, View = WizardModel.ViewModel ,ViewModelWizard=WizardModel};
           
            splitContainerNested.Panel1.Controls.Clear();
            splitContainerNested.Panel1.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text =  "Next" ;
        }

        private void LoadThirdPage()
        {
            var page = new EditColumnsForm() { Dock = DockStyle.Fill, ViewModel = WizardModel.ViewModel };
            splitContainerNested.Panel1.Controls.Clear();
            splitContainerNested.Panel1.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Finish";
        }

        private void SetCurrentStepInfo(string task)
        {
            lblStepInfo.Text = String.Format("Step {0} : {1}", _currentStepNo, task);
            btnBack.Enabled = (_currentStepNo > 1);
        }

        void  GenerateScript()
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int _currentStepNo = 1;

    }
}
