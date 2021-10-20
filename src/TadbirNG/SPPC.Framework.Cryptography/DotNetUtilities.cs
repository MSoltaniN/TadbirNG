using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace SPPC.Framework.Cryptography
{
    internal class DotNetUtilities
    {
        internal static RSA ToRSA(RsaPrivateCrtKeyParameters privateKey)
        {
            return CreateRSAProvider(ToRSAParameters(privateKey));
        }

        internal static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privateKey)
        {
            var rp = default(RSAParameters);
            rp.Modulus = privateKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privateKey.PublicExponent.ToByteArrayUnsigned();
            rp.P = privateKey.P.ToByteArrayUnsigned();
            rp.Q = privateKey.Q.ToByteArrayUnsigned();
            rp.D = ConvertRSAParametersField(privateKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privateKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privateKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privateKey.QInv, rp.Q.Length);
            return rp;
        }

        private static RSA CreateRSAProvider(RSAParameters rp)
        {
            var csp = new CspParameters
            {
                KeyContainerName = String.Format("BouncyCastle-{0}", Guid.NewGuid())
            };
            var rsaCsp = new RSACryptoServiceProvider(csp);
            rsaCsp.ImportParameters(rp);
            return rsaCsp;
        }

        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            byte[] bytes = n.ToByteArrayUnsigned();

            if (bytes.Length == size)
            {
                return bytes;
            }

            if (bytes.Length > size)
            {
                throw new ArgumentException("Specified size is too small.", nameof(size));
            }

            byte[] padded = new byte[size];
            Array.Copy(bytes, 0, padded, size - bytes.Length, bytes.Length);
            return padded;
        }
    }
}