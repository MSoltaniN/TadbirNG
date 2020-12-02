using System;
using System.Collections.Generic;

namespace SPPC.Framework.Cryptography
{
    public interface IEncodedSerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string encoded);
    }
}
