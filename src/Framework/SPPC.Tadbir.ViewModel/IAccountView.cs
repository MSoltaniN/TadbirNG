using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    public interface IAccountView
    {
        string AccountFullCode { get; }

        string DetailAccountFullCode { get; }

        string CostCenterFullCode { get; }

        string ProjectFullCode { get; }
    }
}
