using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Forms
{
    public interface IMetadataEditor
    {
        bool HasMetadata { get; }
        object Metadata { get; }
    }
}
