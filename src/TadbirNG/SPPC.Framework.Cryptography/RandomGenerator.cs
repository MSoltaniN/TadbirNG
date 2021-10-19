using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    ///
    /// </summary>
    public static class RandomGenerator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static byte[] Generate(int byteCount)
        {
            var random = new byte[byteCount];
            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);
            return random;
        }
    }
}
