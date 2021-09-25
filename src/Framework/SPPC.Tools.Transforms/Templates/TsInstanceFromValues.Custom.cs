using System;
using System.Collections.Generic;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class TsInstanceFromValues
    {
        public TsInstanceFromValues(ClientInstanceModel instance)
        {
            _instance = instance;
        }

        private readonly ClientInstanceModel _instance;
    }
}
