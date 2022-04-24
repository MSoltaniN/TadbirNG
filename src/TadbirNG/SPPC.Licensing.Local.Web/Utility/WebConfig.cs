using SPPC.Tadbir.Configuration;

namespace SPPC.Licensing.Local.Web
{
    internal class WebConfig : AppSettingsModel
    {
        public string ServerRoot { get; set; }

        public UnsafeConnection TCP { get; set; }
    }

    internal class UnsafeConnection
    {
        public string Domain { get; set; }

        public int Port { get; set; }
    }
}
