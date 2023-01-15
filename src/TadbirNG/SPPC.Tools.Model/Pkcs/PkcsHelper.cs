using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Tools.Model
{
    public static class PkcsHelper
    {
        public static X509Certificate2 LoadToolsCertificate()
        {
            using var reader = new StreamReader(
                typeof(PkcsHelper).Assembly.GetManifestResourceStream(BinDataUri));
            var binData = reader.ReadToEnd();
            var path = Path.Combine(PathConfig.SolutionRoot, "SPPC.Tools.Model", "Pkcs", "tools.pfx");
            return new X509Certificate2(path, binData);
        }

        private const string BinDataUri = "SPPC.Tools.Model.Pkcs.bin-data";
    }
}
