using System;
using System.Collections.Generic;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class PropertyMetadata
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public string IsRequiredMessage { get; set; }

        public bool HasMinLength { get; set; }

        public string LengthRangeMessage { get; set; }

        public bool HasMaxLength { get; set; }

        public string MaxLengthMessage { get; set; }
    }
}
