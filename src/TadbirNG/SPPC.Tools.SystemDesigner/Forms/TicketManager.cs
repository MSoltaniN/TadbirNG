using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class TicketManager : Form
    {
        public TicketManager()
        {
            InitializeComponent();
        }

        private void ShowValue_Click(object sender, EventArgs e)
        {
            // TEST CODE -- START
            if (!String.IsNullOrEmpty(txtTicket.Text))
            {
                ////var certificate = new X509Certificate2(CertPath, CertPass);
                txtValue.Text = _crypto.Decrypt(txtTicket.Text);
            }
            // TEST CODE -- END
            ////var bytes = Transform.FromBase64String(txtTicket.Text);
            ////var json = Encoding.UTF8.GetString(bytes);
            ////txtValue.Text = json;
        }

        private void ShowTicket_Click(object sender, EventArgs e)
        {
            // TEST CODE -- START
            if (!String.IsNullOrEmpty(txtValue.Text))
            {
                ////var certificate = new X509Certificate2(CertPath, CertPass);
                txtTicket.Text = _crypto.Encrypt(txtValue.Text);
            }
            // TEST CODE -- END
            ////var json = txtValue.Text;
            ////var bytes = Encoding.UTF8.GetBytes(json);
            ////txtTicket.Text = Transform.ToBase64String(bytes);
        }

        private const string CertPath = @"..\..\..\src\TadbirNG\SPPC.Licensing.Local.Web\wwwroot\tadbir.pfx";
        private const string CertPass = "LIdZkifmVG/Uu5Xo6pDk0A==";
        private readonly ICryptoService _crypto = new CryptoService(new CertificateManager());
    }
}
