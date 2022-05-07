using SPPC.Tadbir.Configuration;

namespace SPPC.Tadbir.Web.Api.Internal
{
    internal class WebConfig : AppSettingsModel
    {
        public string ServerRoot { get; set; }

        public JwtConfig Jwt { get; set; }
    }

    internal class JwtConfig
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int Expiration { get; set; }

        public string Secret { get; set; }
    }
}
