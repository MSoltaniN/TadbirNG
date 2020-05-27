using System;
using BabakSoft.Platform.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using System.IO;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using BabakSoft.Platform.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class SysViewMoldelsForm : UserControl
    {
        public SysViewMoldelsForm()
        {
            InitializeComponent();
            Info = "Registerd View Models ";
            _sysConnection = GetSysConnectionString();
            cboxModelViewSelector.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public string Info { get; set; }
        public SysVeiwModel SysView { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            int SelectedIndex=0;
            if (SysView.SysModelViewItemIndex == -1)
            {
                var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
                _ViewModelList = dal.Query("SELECT v.ViewID,v.Name FROM Metadata.[View] v");
            }
            else
            {
                SelectedIndex  = SysView.SysModelViewItemIndex;
                _ViewModelList = SysView.SysModelViewItems;
            }
            cboxModelViewSelector.ValueMember = "ViewID";
            cboxModelViewSelector.DisplayMember = "Name";
            cboxModelViewSelector.DataSource = _ViewModelList;
            cboxModelViewSelector.SelectedIndex = SelectedIndex;


        }

       
        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }



        private void cboxModelViewSelector_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int nSelectedViewId = cboxModelViewSelector.SelectedIndex;
            
            var strQuery = string.Format(@"SELECT 
                                          c.ColumnID
                                         ,c.Name
                                         ,c.Type
                                         ,c.Length
                                         ,c.IsNullable
                                         ,ISNULL(c.Visibility,'Visibale') AS Visibility
                                         ,c.DisplayIndex
                                        FROM Metadata.[Column] c WHERE c.ViewID={0}", nSelectedViewId);

            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            var result = dal.Query(strQuery);

            GVMetaDataViewer.DataSource = result;
        }


      private void SysViewMoldelsForm_Leave(object sender, EventArgs e)
        {
            if (cboxModelViewSelector.SelectedItem != null)
            {
                SysView.SysModelViewItemIndex = cboxModelViewSelector.SelectedIndex;
                SysView.SysModelViewItems = _ViewModelList;
            }
        }
        private string _sysConnection = "";
        private DataTable _ViewModelList;
    }
}
