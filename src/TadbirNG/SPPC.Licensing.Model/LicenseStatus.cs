using System;

namespace SPPC.Licensing.Model
{
    public enum LicenseStatus
    {
        OK = 0,                 // License is valid and has no problems
        NoLicense = 1,          // License file is moved or doesn't exist
        Corrupt = 2,            // License file is corrupt or tampered with
        NoCertificate = 3,      // Licensing certificate is missing or unavailable
        BadCertificate = 4,     // Licensing certificate has been replaced
        HardwareMismatch = 5,   // Hardware ID does not match with the ID from activation
        InstanceMismatch = 6,   // Instance ID does not match with activated instance ID
        Expired = 7,            // License file is OK but the license is expired
    }
}
