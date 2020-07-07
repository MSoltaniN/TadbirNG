using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    public partial class PermissionDesignerForm : Form
    {
        public PermissionDesignerForm()
        {
            InitializeComponent();
            PermissionDesignerModel = new PermissionDesignerModel();
        }
        public PermissionDesignerModel PermissionDesignerModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _sysConnection = GetSysConnectionString();
            SetupBindings();
            lboxPermissions.DisplayMember = "Name";
        }

        private void SetupBindings()
        {
            txtPermissionGroupName.DataBindings.Add("Text", PermissionDesignerModel.PermissionGroup, "Name");
            txtPermissionGroupEntityName.DataBindings.Add("Text", PermissionDesignerModel.PermissionGroup, "EntityName");
            txtPermissionGroupDescription.DataBindings.Add("Text", PermissionDesignerModel.PermissionGroup, "Description");
        }
        private void ApplyPermissionGroup_Click(object sender, EventArgs e)
        {
            if(txtPermissionGroupName.Text == "")
            {
                MessageBox.Show("Please enter a name for permission group.");
                return;
            }
            if(btnApplyPermissionGroup.Text == "->")
            {
                ApplyPermissionGroup();
                LoadDefaultPermissions(PermissionDesignerModel.Permissions.Count == 0);
                LoadPermissionsList();
                lboxPermissions.SelectedIndex = 0;
                gboxPermissionGroup.Enabled = false;
                gboxPermission.Enabled = true;
                lboxPermissions.Enabled = true;
                btnApplyPermissionGroup.Text = "<-";
            }
            else
            {
                lboxPermissions.Items.Clear();
                gboxPermissionGroup.Enabled = true;
                gboxPermission.Enabled = false;
                lboxPermissions.Enabled = false;
                btnApplyPermissionGroup.Text = "->";
            }
            
        }
        private void ApplyPermissionGroup()
        {
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            var lastId = dal.QueryScalar("SELECT MAX(pg.PermissionGroupID) FROM [Auth].[PermissionGroup] pg");
               
            PermissionDesignerModel.PermissionGroup.Id = Convert.ToInt32(lastId)+1;
            PermissionDesignerModel.PermissionGroup.Name = txtPermissionGroupName.Text;
            PermissionDesignerModel.PermissionGroup.EntityName = txtPermissionGroupEntityName.Text;
            PermissionDesignerModel.PermissionGroup.Description = txtPermissionGroupDescription.Text;
        }

        private void  LoadDefaultPermissions(bool IsNotFirstLoad)
        {
            if(IsNotFirstLoad)
            {
                List<string> PermissionNames = new List<string> { "View", "Create", "Edit", "Delete", "Filter", "Print" };
                int itemIndex = 0;
                foreach(var item in PermissionNames)
                {
                    var permission = GetPermission(item, itemIndex++);
                    PermissionDesignerModel.Permissions.Add(permission);
                }
            }
        }
        private PermissionViewModel GetPermission(string name,int index)
        {
            var Permission = new PermissionViewModel()
            {
                Id=index,
                Name = name,
                Flag = (int)Math.Pow(2, index),
                Description = "",
            };
            return Permission;
        }
        private void LoadPermissionsList()
        {
            foreach (var item in PermissionDesignerModel.Permissions)
            {
                lboxPermissions.Items.Add(item);
            }
        }
       
        private void Permissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lboxPermissions.SelectedItem == null)
            {
                return;
            }
            if(_lastselectedIndex != lboxPermissions.SelectedIndex && lboxPermissions.SelectedIndex != -1)
            {
                SaveLastPermissionDetails(_lastselectedIndex != -1);
                RefereshPermissionList();
                _lastselectedIndex = lboxPermissions.SelectedIndex;
                RetrieveLastPermissionDetails(_lastselectedIndex != -1);
                ShowFlagLable();
            }
        }
        private void SaveLastPermissionDetails(bool dataExist)
        {
            if(dataExist)
            {
                PermissionDesignerModel.Permissions[_lastselectedIndex].Name = txtPermissionName.Text;
                PermissionDesignerModel.Permissions[_lastselectedIndex].Flag = Convert.ToInt32(Math.Pow(2, trbFlag.Value));
                PermissionDesignerModel.Permissions[_lastselectedIndex].Description = txtPermissionDesc.Text;
            }
        }
        private void RetrieveLastPermissionDetails(bool dataExist)
        {
            if(dataExist)
            {
                txtPermissionName.Text = PermissionDesignerModel.Permissions[_lastselectedIndex].Name;
                trbFlag.Value = Convert.ToInt32(Math.Log(PermissionDesignerModel.Permissions[_lastselectedIndex].Flag, 2));
                txtPermissionDesc.Text = PermissionDesignerModel.Permissions[_lastselectedIndex].Description;
            }
        }
        private void RefereshPermissionList()
        {
            if (_lastselectedIndex != -1)
            {
                lboxPermissions.Items.RemoveAt(_lastselectedIndex);
                lboxPermissions.Items.Insert(_lastselectedIndex, PermissionDesignerModel.Permissions[_lastselectedIndex]);
            }
        }
        private void ShowFlagLable()
        {
            var flagValue = Math.Pow(2, trbFlag.Value).ToString();
            lblFlag.Text = "Flag:" + flagValue;
        }
        private void PermissionName_Leave(object sender, EventArgs e)
        {
            if (txtPermissionName.Text == "")
            {
                MessageBox.Show("Please don't leave this field empty");
            }
        }
        private void Flag_Scroll(object sender, EventArgs e)
        {
            ShowFlagLable();
        }
        private void AddPermission_Click(object sender, EventArgs e)
        {
            var newItemIndex = lboxPermissions.Items.Count;
            var permission = GetPermission(string.Format("(Pemission {0})", newItemIndex + 1), newItemIndex);
            PermissionDesignerModel.Permissions.Add(permission);
            lboxPermissions.Items.Add(permission);
            lboxPermissions.SelectedIndex = newItemIndex;
        }
        private void DeletePermission_Click(object sender, EventArgs e)
        {
            if(lboxPermissions.SelectedIndex != -1)
            {
                var selectedIndex = lboxPermissions.SelectedIndex;
                var permissionItemCount = lboxPermissions.Items.Count;
                PermissionDesignerModel.Permissions.RemoveAt(selectedIndex);
                lboxPermissions.Items.Clear();
                LoadPermissionsList();
                lboxPermissions.SelectedIndex = _lastselectedIndex 
                                        = (selectedIndex + 1 == permissionItemCount ? _lastselectedIndex -1 : _lastselectedIndex);
                RetrieveLastPermissionDetails(_lastselectedIndex != -1);
            }
        }
        private void GenarateScript_Click(object sender, EventArgs e)
        {
            if (!VerifyFields())
            {
                return;
            }
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            int maxPermissionGroupId = Convert.ToInt32(dal.QueryScalar("SELECT MAX(pg.PermissionGroupID) FROM [Auth].[PermissionGroup] pg"));
            var pg = PermissionDesignerModel.PermissionGroup;
            var Permissions = PermissionDesignerModel.Permissions;
            var builder = new StringBuilder();
            var solutionVersion = GetSolutionVersion();

            int columnId = 0;
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendFormat("-- {0}", solutionVersion);
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] ON ");
            builder.AppendFormat("INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName] , [Description]) VALUES ({0}, N'{1}', N'{2}' , N'{3}')"
                                    , maxPermissionGroupId + 1
                                    , pg.Name
                                    , pg.EntityName
                                    , pg.Description);

            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF ");
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[Permission] ON ");

            foreach(var item in Permissions)
            {
                builder.AppendFormat("INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag] , [Description]) VALUES ({0}, {1}, N'{2}', {3}, N'{4}')"
                                      , columnId++
                                      , maxPermissionGroupId + 1
                                      , item.Name
                                      , item.Flag
                                      , item.Description);
                builder.AppendLine();
            }
            

            builder.AppendLine("SET IDENTITY_INSERT[Auth].[Permission] OFF ");
            builder.AppendLine();

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());

            MessageBox.Show("the script was generated. ");
        }
        private bool VerifyFields()
        {
            bool res = true;
            if(lboxPermissions.Items.Count == 0)
            {
                MessageBox.Show("Please enter at least one Permission");
                res = false;
            }
            return res;
        }
        private Version GetSolutionVersion()
        {
            var assemblyVersion = GetType().Assembly.GetName().Version;
            return new Version(assemblyVersion.ToString());
        }
        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
       

        private string _sysConnection;
        static int _lastselectedIndex = -1;
        private const string _TadbirSysUpdateScript = @"..\..\res\TadbirSys_UpdateDbObjects.sql";

       
    }
}
