using System;

namespace SPPC.Tools.Model.Project
{
    public class EntityRule
    {
        public string FieldName { get; set; }

        public bool Required { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }
    }
}
