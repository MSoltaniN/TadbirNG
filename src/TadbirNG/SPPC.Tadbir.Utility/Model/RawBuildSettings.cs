using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility.Model
{
    public class RawBuildSettings : IBuildSettings
    {
        public RawBuildSettings()
        {
            Tcp = new RemoteConnection();
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
