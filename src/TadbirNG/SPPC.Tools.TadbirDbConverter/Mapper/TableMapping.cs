using System.Collections.Generic;

namespace SPPC.Tools.TadbirDbConverter
{
    public class TableMapping
    {
        public TableMapping()
        {
            Fields = new List<FieldMapping>();
            IgnoreFields = new List<string>();
        }

        public string SourceTable { get; set; }

        public string TargetSchema { get; set; }

        public bool HadFpId { get; set; }

        public IList<FieldMapping> Fields { get; }

        public IList<string> IgnoreFields { get; }
    }
}
