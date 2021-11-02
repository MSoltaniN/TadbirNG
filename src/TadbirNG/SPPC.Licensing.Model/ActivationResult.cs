using System;

namespace SPPC.Licensing.Model
{
    public enum ActivationResult
    {
        OK = 0,                 // Activation succeeded without any errors
        AlreadyActivated = 1,   // License is already activated
        BadInstance = 2,        // Instance information is invalid or unknown
        Failed = 3              // Activation failed due to an internal problem
    }
}
