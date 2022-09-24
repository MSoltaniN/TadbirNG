using System;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// کلاس کمکی برای دسترسی آسان به انواع تنظیمات آماده برای برنامه
    /// </summary>
    public static class BuildSettings
    {
        static BuildSettings()
        {
            WebLocal = new WebLocalSettings();
            WebNetwork = new WebNetworkSettings();
            DockerLocal = new DockerLocalSettings();
            DockerNetwork = new DockerNetworkSettings();
            DockerDummy = new DockerDummySettings();
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

        /// <summary>
        /// تنظیمات مورد استفاده برای ساخت فایل های پایه برای محیط داکر
        /// </summary>
        public static IBuildSettings DockerDummy { get; }

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
                DbUserName = SysParameterUtility.AllParameters.Db.LoginName;
                DbPassword = SysParameterUtility.AllParameters.Db.Password;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string SaPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class WebNetworkSettings : IBuildSettings
        {
            public WebNetworkSettings()
            {
                var winHost = SysParameterUtility.Servers.WinIpAddress;
                OnlineServerRoot = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.LocalHostUrl, Port = 5555 };
                DbServerName = winHost;
                DbUserName = SysParameterUtility.AllParameters.Db.LoginName;
                DbPassword = SysParameterUtility.AllParameters.Db.Password;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string SaPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerLocalSettings : IBuildSettings
        {
            public DockerLocalSettings()
            {

                var winHost = SysParameterUtility.Servers.WinIpAddress;
                OnlineServerRoot = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.DockerHostInternalUrl, Port = 5555 };
                DbServerName = SysParameterUtility.DbServer.Name;
                DbUserName = SysParameterUtility.AllParameters.Db.LoginName;
                DbPassword = SysParameterUtility.AllParameters.Db.Password;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string SaPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerNetworkSettings : IBuildSettings
        {
            public DockerNetworkSettings()
            {
                var winHost = SysParameterUtility.Servers.WinIpAddress;
                var linuxHost = SysParameterUtility.Servers.LinuxIpAddress;
                OnlineServerRoot = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{linuxHost}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{linuxHost}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = winHost, Port = 5555 };
                DbServerName = SysParameterUtility.DbServer.Name;
                DbUserName = SysParameterUtility.AllParameters.Db.LoginName;
                DbPassword = SysParameterUtility.AllParameters.Db.Password;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string SaPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }

        private class DockerDummySettings : IBuildSettings
        {
            public DockerDummySettings()
            {
                var winHost = SysParameterUtility.Servers.WinIpAddress;
                OnlineServerRoot = String.Format(
                    $"http://{winHost}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Tcp = new RemoteConnection() { Domain = BuildSettingValues.LocalHostUrl, Port = 5555 };
                DbServerName = SysParameterUtility.DbServer.Name;
                DbUserName = "User";
                DbPassword = "1234";
                Key = BuildSettingValues.DummyInstanceKey;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Tcp { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string SaPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }
    }
}
