using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public sealed class Constants
    {
        private Constants()
        {
        }

        public const string SubjectName = "CN=Tadbir Licensing CA";
        public const string CertificateFile = "tadbir.pfx";
        public const string LicenseFile = "tadbir.lic";
        public const string InstanceHeaderName = "X-Tadbir-Instance";
        public const string LicenseHeaderName = "X-Tadbir-License";
        public const string LicenseCheckHeaderName = "X-Tadbir-LicenseCheck";
    }
}
