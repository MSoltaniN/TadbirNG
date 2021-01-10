using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SPPC.Framework.Cryptography
{
    public static class RandomGenerator
    {
        public static byte[] Generate(int byteCount)
        {
            var random = new byte[byteCount];
            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);
            return random;
        }
    }
}
