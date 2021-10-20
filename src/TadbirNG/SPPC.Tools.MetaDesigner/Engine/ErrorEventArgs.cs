using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(Exception exception)
        {
            this.Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
