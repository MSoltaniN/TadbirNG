using System;
using System.IO;
using SPPC.Framework.Cryptography;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class ApiServiceSetup : DockerServiceSetup
    {
        public ApiServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => "api-server";

        protected override ITextTemplate SettingsTemplate => new WebApiSettings(_settings);

        protected override void ConfigureAppLayer(string layerId)
        {
            base.ConfigureAppLayer(layerId);
            var source = Path.Combine(RootFolder, "license");
            var licenseInfo = _crypto.Decrypt(File.ReadAllText(source));
            var licensePath = Path.Combine(Environment.CurrentDirectory, layerId, "app", "wwwroot", "license");
            File.WriteAllText(licensePath, licenseInfo);
        }

        private readonly ICryptoService _crypto = new CryptoService(new CertificateManager());
    }
}
