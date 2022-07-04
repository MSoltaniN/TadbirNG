using System;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Model
{
    public static class BuildSettings
    {
        static BuildSettings()
        {
            WebLocal = new WebLocalSettings();
            WebNetwork = new WebNetworkSettings();
            DockerLocal = new DockerLocalSettings();
            DockerNetwork = new DockerNetworkSettings();
            Docker = new DockerSettings();
        }

        /// <summary>
        /// تنظیمات مورد استفاده برای اجرای برنامه بدون داکر روی سیستم توسعه دهنده
        /// </summary>
        public static IBuildSettings WebLocal { get; }

        /// <summary>
        /// تنظیمات مورد استفاده برای اجرای برنامه بدون داکر روی سرور مجازی ویندوز
        /// </summary>
        public static IBuildSettings WebNetwork { get; }

        /// <summary>
        /// تنظیمات مورد استفاده برای اجرای برنامه در محیط داکر - قابل دسترسی فقط روی سرور اصلی
        /// </summary>
        public static IBuildSettings DockerLocal { get; }

        /// <summary>
        /// تنظیمات مورد استفاده برای اجرای برنامه در محیط داکر - قابل دسترسی در شبکه
        /// </summary>
        public static IBuildSettings DockerNetwork { get; }

        public static IBuildSettings Docker { get; }

        private class WebLocalSettings : IBuildSettings
        {
            public WebLocalSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:1447");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:7473");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:7473");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:8801");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.LocalHostUrl, Port = 5555 };
                DbServerName = Environment.MachineName;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class WebNetworkSettings : IBuildSettings
        {
            public WebNetworkSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.LocalHostUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DefaultWinHostUrl;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerLocalSettings : IBuildSettings
        {
            public DockerLocalSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.DockerHostInternalUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DockerDbServer;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerNetworkSettings : IBuildSettings
        {
            public DockerNetworkSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultLinHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultLinHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.DefaultWinHostUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DockerDbServer;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerSettings : IBuildSettings
        {
            public DockerSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultWinHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.DockerHostInternalUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DockerDbServer;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }
    }
}
