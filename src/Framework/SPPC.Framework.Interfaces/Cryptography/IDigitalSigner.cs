using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    public interface IDigitalSigner
    {
        X509Certificate2 Certificate { get; set; }

        string SignData(byte[] data);

        bool VerifyData(byte[] data, string signature);
    }
}
