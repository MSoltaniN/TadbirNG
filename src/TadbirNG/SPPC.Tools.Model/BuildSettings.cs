using System;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Model
{
    public static class BuildSettings
    {
        static BuildSettings()
        {
            Default = new DefaultSettings();
            Docker = new DockerSettings();
        }

        public static IBuildSettings Default { get; }

        public static IBuildSettings Docker { get; }

        private class DefaultSettings : IBuildSettings
        {
            public DefaultSettings()
            {
                OnlineServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DefaultHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.DefaultHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Ssh = new RemoteConnection() { Domain = BuildSettingValues.LocalHostUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DefaultHostUrl;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Ssh { get; }

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
                    $"http://{BuildSettingValues.DefaultHostUrl}:{BuildSettingValues.DefaultWebLicenseApiPort}");
                LocalServerRoot = String.Format(
                    $"http://{BuildSettingValues.DockerHostInternalUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                LocalServerUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultLicenseApiPort}");
                WebApiUrl = String.Format(
                    $"http://{BuildSettingValues.LocalHostUrl}:{BuildSettingValues.DefaultApiPort}");
                Ssh = new RemoteConnection() { Domain = BuildSettingValues.DockerHostInternalUrl, Port = 5555 };
                DbServerName = BuildSettingValues.DockerDbServer;
                DbUserName = BuildSettingValues.DefaultDbUser;
                DbPassword = BuildSettingValues.DefaultDbPassword;
            }

            public string OnlineServerRoot { get; set; }

            public string LocalServerRoot { get; set; }

            public string LocalServerUrl { get; set; }

            public string WebApiUrl { get; set; }

            public RemoteConnection Ssh { get; }

            public string DbServerName { get; set; }

            public string DbUserName { get; set; }

            public string DbPassword { get; set; }

            public string Key { get; set; }

            public string Version { get; set; }
        }
    }
}
