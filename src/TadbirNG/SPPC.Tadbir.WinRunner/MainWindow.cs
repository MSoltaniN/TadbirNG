using System;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.WinRunner
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            _runner = new CliRunner();
            _runner.OutputReceived += Runner_OutputReceived;
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            txtConsole.Text = e.Output;
        }

        private void Install_Click(object sender, EventArgs e)
        {
            var command = "sc create \"sppckeysrv\" type= own start= auto error= normal displayname= \"SPPC Key Server\" binpath= \"C:\\SPPC\\TadbirNG\\Service\\SPPC.Framework.KeyServer.exe\"";
            RunConsoleCommand(command);
        }

        private void StartService_Click(object sender, EventArgs e)
        {
            RunConsoleCommand("sc start \"sppckeysrv\"");
        }

        private void Pull_Click(object sender, EventArgs e)
        {
            txtConsole.Text = String.Empty;
            RunConsoleCommand("docker pull msn1368/license-server:dev");
            RunConsoleCommand("docker pull msn1368/api-server:latest", true);
            RunConsoleCommand("docker pull msn1368/web-app:dev", true);
            RunConsoleCommand("docker pull msn1368/db-server:dev", true);
        }

        private void StartContainers_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RunConsoleCommand(string command, bool append = false)
        {
            Cursor = Cursors.AppStarting;
            txtConsole.Focus();
            if (!append)
            {
                _builder.Clear();
            }

            _builder.Append(_runner.Run(command).Trim());
            _builder.AppendLine().AppendLine();
            txtConsole.Text = _builder.ToString();
            txtConsole.SelectionStart = txtConsole.Text.Length;
            Cursor = Cursors.Default;
        }

        private readonly CliRunner _runner;
        private readonly StringBuilder _builder = new();
    }
}
