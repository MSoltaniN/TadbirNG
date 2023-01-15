using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class TicketManager : Form
    {
        public TicketManager()
        {
            InitializeComponent();
        }

        private enum OperationMode
        {
            Simple = 0,
            PGP = 1,
            UrlEncoded = 2
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cmbOpMode.SelectedIndex = 0;
            _certificate = PkcsHelper.LoadToolsCertificate();
        }

        private void ShowValue_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTicket.Text))
            {
                return;
            }

            var mode = (OperationMode)cmbOpMode.SelectedIndex;
            switch (mode)
            {
                case OperationMode.Simple:
                    txtValue.Text = SimpleDecrypt(txtTicket.Text);
                    break;
                case OperationMode.PGP:
                    txtValue.Text = StrongDecrypt(txtTicket.Text);
                    break;
                case OperationMode.UrlEncoded:
                    txtValue.Text = UrlEncodedDecrypt(txtTicket.Text);
                    break;
            }
        }

        private void ShowTicket_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtValue.Text))
            {
                return;
            }

            var mode = (OperationMode)cmbOpMode.SelectedIndex;
            switch (mode)
            {
                case OperationMode.Simple:
                    txtTicket.Text = SimpleEncrypt(txtValue.Text);
                    break;
                case OperationMode.PGP:
                    txtTicket.Text = StrongEncrypt(txtValue.Text);
                    break;
                case OperationMode.UrlEncoded:
                    txtTicket.Text = UrlEncodedEncrypt(txtValue.Text);
                    break;
            }
        }

        private static string UrlEncodedEncrypt(string data)
        {
            return Transform.ToBase64String(
                Encoding.UTF8.GetBytes(
                    WebUtility.UrlEncode(data)));
        }

        private static string UrlEncodedDecrypt(string cipher)
        {
            return WebUtility.UrlDecode(
                Encoding.UTF8.GetString(
                    Transform.FromBase64String(cipher)));
        }

        private string SimpleEncrypt(string data)
        {
            return _crypto.Encrypt(data);
        }

        private string SimpleDecrypt(string cipher)
        {
            return _crypto.Decrypt(cipher);
        }

        private string StrongEncrypt(string data)
        {
            return _crypto.Encrypt(data, _certificate);
        }

        private string StrongDecrypt(string cipher)
        {
            return _crypto.Decrypt(cipher, _certificate);
        }

        private readonly ICryptoService _crypto = new CryptoService(new CertificateManager());
        private X509Certificate2 _certificate;
    }
}
