﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Persistence;

namespace SPPC.Tools.SystemDesigner.Designers
{
    public partial class ReportEditorForm : Form
    {
        public ReportEditorForm()
        {
            InitializeComponent();
            Parameters = new DataTable("ParameterTable");
        }

        public string SysConnection { get; set; }

        public DataTable Parameters { get; set; }

        public void SetupControls()
        {
            LoadListViews();
            LoadSubSystems();
            LoadParents();
            LoadParameters();
        }

        #region General tab

        private void LoadListViews()
        {
            var dal = new SqlDataLayer(SysConnection);
            cmbListViews.ValueMember = "ViewID";
            cmbListViews.DisplayMember = "Name";
            cmbListViews.DataSource = dal.Query(
                @"SELECT [ViewID], [Name] FROM [Metadata].[View] ORDER BY [Name]");
        }

        private void LoadSubSystems()
        {
            cmbSubsystem.SelectedIndex = 0;
        }

        private void LoadParents()
        {
            var dal = new SqlDataLayer(SysConnection);
            cmbParent.ValueMember = "ReportID";
            cmbParent.DisplayMember = "Code";
            cmbParent.DataSource = dal.Query(@"SELECT [ReportID],[Code] FROM [Reporting].[Report] 
                                                      WHERE [IsGroup] = 1 ORDER BY [ReportID]");
        }
       
        private void Select_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "Browse Template File";
            fileDialog.DefaultExt = "mrt";
            fileDialog.Filter = "mrt files (*.mrt)|*.mrt|All files (*.*)|*.*";
            if( fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplateEn.Text = fileDialog.FileName;
            }
        }

        private void BrowseFa_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "Browse Template File";
            fileDialog.DefaultExt = "mrt";
            fileDialog.Filter = "mrt files (*.mrt)|*.mrt|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplateFa.Text = fileDialog.FileName;
            }
        }

        private void QuickReport_CheckedChanged(object sender, EventArgs e)
        {
           if( chkQuickReport.Checked == true )
            {
                txtTemplateEn.Enabled = false;
                btnBrowseEn.Enabled = false;
                txtTemplateFa.Enabled = false;
                btnBrowseFa.Enabled = false;
            }
           else
            {
                txtTemplateEn.Enabled = true;
                btnBrowseEn.Enabled = true;
                txtTemplateFa.Enabled = true;
                btnBrowseFa.Enabled = true;
            }
        }

        #endregion

        #region Parameter tab

        private void LoadParameters()
        {
            MakeDataTableParameter();
            RefreshGrid();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var paramForm = new ParameterEditorForm();
            paramForm.SetupConrols();

            if (paramForm.ShowDialog() == DialogResult.OK )
            {
                DataRow dr;
                dr = Parameters.NewRow();
                dr["Name"] = paramForm.txtName.Text;
                dr["FieldName"] = paramForm.txtFieldName.Text;
                dr["CaptionKey"] = paramForm.txtCaptionKey.Text;
                dr["Operator"] = paramForm.cmbOperator.SelectedItem.ToString();
                dr["DataType"] = paramForm.cmbDataType.SelectedItem.ToString();
                dr["ControlType"] = paramForm.cmbControlType.SelectedItem.ToString();
                Parameters.Rows.Add(dr);
                RefreshGrid();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            int paramId=1;
            if (grdParameters.SelectedRows.Count > 0)
            {
                paramId = Convert.ToInt32(grdParameters.SelectedRows[0].Cells["ParamId"].Value.ToString());
                DataRow dr = Parameters.Select(string.Format("ParamId={0}", paramId)).FirstOrDefault();
                if (dr != null)
                {
                    var paramForm = new ParameterEditorForm();
                    paramForm.SetupConrols();
                    paramForm.txtName.Text = dr["Name"].ToString();
                    paramForm.txtFieldName.Text = dr["FieldName"].ToString();
                    paramForm.txtCaptionKey.Text = dr["CaptionKey"].ToString();
                    paramForm.cmbOperator.SelectedItem = dr["Operator"].ToString();
                    paramForm.cmbDataType.SelectedItem = dr["DataType"].ToString();
                    paramForm.cmbControlType.SelectedItem = dr["ControlType"].ToString();

                    if (paramForm.ShowDialog() == DialogResult.OK)
                    {
                        dr["Name"] = paramForm.txtName.Text;
                        dr["FieldName"] = paramForm.txtFieldName.Text;
                        dr["CaptionKey"] = paramForm.txtCaptionKey.Text;
                        dr["Operator"] = paramForm.cmbOperator.SelectedItem.ToString();
                        dr["DataType"] = paramForm.cmbDataType.SelectedItem.ToString();
                        dr["ControlType"] = paramForm.cmbControlType.SelectedItem.ToString();
                        RefreshGrid();
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (grdParameters.SelectedRows.Count > 0)
            {
                int paramId = Convert.ToInt32(grdParameters.SelectedRows[0].Cells["ParamId"].Value.ToString());
                DataRow dr = Parameters.Select(string.Format("ParamId={0}", paramId)).FirstOrDefault();
                dr.Delete();
                RefreshGrid();
            }
        }

        private void MakeDataTableParameter()
        {
            if (Parameters.Columns.Count > 0)
            {
                return;
            }

            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "ParamId",
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                ReadOnly = true,
                Unique = true
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Name",
                AutoIncrement = false,
                Caption = "Name",
                ReadOnly = false,
                Unique = false
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "FieldName",
                AutoIncrement = false,
                Caption = "FieldName",
                ReadOnly = false,
                Unique = false
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "CaptionKey",
                AutoIncrement = false,
                Caption = "CaptionKey",
                ReadOnly = false,
                Unique = false
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Operator",
                AutoIncrement = false,
                Caption = "Operator",
                ReadOnly = false,
                Unique = false
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "DataType",
                AutoIncrement = false,
                Caption = "DataType",
                ReadOnly = false,
                Unique = false
            });
            Parameters.Columns.Add(new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "ControlType",
                AutoIncrement = false,
                Caption = "ControlType",
                ReadOnly = false,
                Unique = false
            });

            var PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = Parameters.Columns["id"];
            Parameters.PrimaryKey = PrimaryKeyColumns;
        }

        public void RefreshGrid()
        {
            grdParameters.DataSource = Parameters;
        }

        #endregion

        private void Save_Click(object sender, EventArgs e)
        {
            if(!Validate())
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private new bool Validate()
        {
            if (txtEnglish.Text == "")
            {
                MessageBox.Show("Please fill English Caption.");
                return false;
            }
            if (txtPersian.Text == "")
            {
                MessageBox.Show("Please fill Persian Caption.");
                return false;
            }
            ////if ((txtTemplateEn.Text == "" || txtTemplateFa.Text == "" ) && chkQuickReport.Checked == false )
            ////{
            ////    MessageBox.Show("Please fill template file path.");
            ////    return false;
            ////}
            return true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
