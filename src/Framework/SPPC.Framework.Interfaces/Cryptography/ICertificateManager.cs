using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    public interface ICertificateManager
    {
        X509Certificate2 GenerateSelfSigned(string issuerName, string subjectName);

        void AddToStore(X509Certificate2 cert, StoreName st, StoreLocation sl);

        X509Certificate2 GetFromStore(string issuerName);

        X509Certificate2 GetFromFile(string path, string password);
    }
}
