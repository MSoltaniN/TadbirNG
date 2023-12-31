﻿using System;
using System.Windows.Forms;
using SPPC.Tadbir.Utility;

namespace SPPC.Tools.LicenseManager
{
    public partial class InstanceInfoEditor : Form
    {
        public InstanceInfoEditor()
        {
            InitializeComponent();
        }

        public IBuildSettings BuildSettings { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void SaveInstanceButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInstance())
            {
                return;
            }

            Close();
        }

        private void SetupBindings()
        {
            txtBaseUrl.DataBindings.Add("Text", BuildSettings, "WebApiUrl");
            txtServerUrl.DataBindings.Add("Text", BuildSettings, "LocalServerRoot");
            txtOnlineServerUrl.DataBindings.Add("Text", BuildSettings, "OnlineServerRoot");
            txtInstanceKey.DataBindings.Add("Text", BuildSettings, "Key");
            txtVersion.DataBindings.Add("Text", BuildSettings, "Version");
        }

        private bool ValidateInstance()
        {
            if (!EnsureNotEmpty(txtBaseUrl, "آدرس سرویس وب"))
            {
                return false;
            }

            if (!EnsureNotEmpty(txtServerUrl, "آدرس سرور آفلاین مجوزها"))
            {
                return false;
            }

            return true;
        }

        private bool EnsureNotEmpty(TextBox textBox, string field)
        {
            bool isValid = true;
            string template = "وارد کردن {0} اجباری است.";
            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                string message = String.Format(template, field);
                MessageBox.Show(this, message, "پیغام خطا", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                isValid = false;
            }

            return isValid;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
