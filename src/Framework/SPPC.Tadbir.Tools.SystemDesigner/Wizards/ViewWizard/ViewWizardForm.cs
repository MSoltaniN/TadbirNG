using BabakSoft.Platform.Data;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class ViewWizardForm : Form
    {
        public ViewWizardForm()
        {
            InitializeComponent();

            _sysConnection = GetSysConnectionString();

            cboxModelViewSelector.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ViewWizardForm_Load(object sender, EventArgs e)
        {
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            var result = dal.Query("SELECT v.ViewID,v.Name FROM Metadata.[View] v");

            cboxModelViewSelector.ValueMember = "ViewID";
            cboxModelViewSelector.DisplayMember = "Name";
            cboxModelViewSelector.DataSource=result;
        }
        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private void cboxModelViewSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelectedViewId=cboxModelViewSelector.SelectedIndex;
            var strQuery= string.Format(@"SELECT 
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

        private  string _sysConnection = "";
    }
}
