﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.WinRunner
{
    public partial class RunnerForm : Form
    {
        public RunnerForm()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (MessageBox.Show("می خواهید سرویس های برنامه را متوقف کنید؟",
                "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                CleanStop();
            }
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (!String.IsNullOrEmpty(e.Output))
                    {
                        txtConsole.AppendText(e.Output
                            .Replace("\n", Environment.NewLine)
                            .Trim());
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.ScrollToCaret();
                    }
                });
            }
        }

        private void RunApp_Click(object sender, EventArgs e)
        {
            txtConsole.Focus();
            _runner.OutputReceived += Runner_OutputReceived;
            btnRunApp.Enabled = false;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _runner.Run(String.Format($"{ComposeCommand} down"));
            _runner.Run(String.Format($"{ComposeCommand} up --no-build"));
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("می خواهید سرویس های برنامه را متوقف کنید؟",
                "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                CleanStop();
                Application.Exit();
            }
        }

        private void CleanStop()
        {
            _runner.Stop();
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }

        private const string ComposeCommand = "docker-compose -f docker-compose.override.yml -f docker-compose.yml";
        private readonly CliRunner _runner = new();
    }
}
