using System;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public OutputReceivedEventArgs(string output)
        {
            Output = output;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Output { get; }
    }
}
