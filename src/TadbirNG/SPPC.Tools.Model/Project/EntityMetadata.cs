using System.Collections.Generic;

namespace SPPC.Tools.Model.Project
{
    public class EntityMetadata
    {
        public EntityMetadata()
        {
            Bindings = new List<EntityBinding>();
            Rules = new List<EntityRule>();
        }

        public string Type { get; set; }

        public List<EntityBinding> Bindings { get; }

        public List<EntityRule> Rules { get; }
    }
}
