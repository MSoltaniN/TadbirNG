using System;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;

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
            var bytes = Transform.FromBase64String(txtTicket.Text);
            var json = Encoding.UTF8.GetString(bytes);
            txtValue.Text = json;
        }

        private void ShowTicket_Click(object sender, EventArgs e)
        {
            var json = txtValue.Text;
            var bytes = Encoding.UTF8.GetBytes(json);
            txtTicket.Text = Transform.ToBase64String(bytes);
        }
    }
}
