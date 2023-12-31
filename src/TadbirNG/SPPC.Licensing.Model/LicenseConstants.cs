﻿using System;

namespace SPPC.Licensing.Model
{
    public sealed class LicenseConstants
    {
        private LicenseConstants()
        {
        }

        public const int IssuerKeySizeInBits = 4096;
        public const int CertKeySizeInBits = 2048;
        public const string IssuerName = "CN=Tadbir Licensing CA";
        public const string SubjectName = "CN=Tadbir";
        public const string CertificateFile = "tadbir.pfx";
        public const string LicenseFile = "tadbir.lic";
        public const string InstanceHeaderName = "X-Tadbir-Instance";
        public const string LicenseHeaderName = "X-Tadbir-License";
        public const string LicenseCheckHeaderName = "X-Tadbir-LicenseCheck";

        public static TimeSpan SessionTimeout
        {
            get { return TimeSpan.FromDays(14); }
        }
    }
}
