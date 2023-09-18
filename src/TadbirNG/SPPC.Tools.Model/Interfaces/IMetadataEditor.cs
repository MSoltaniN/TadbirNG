using System;

namespace SPPC.Tools.Presentation
{
    public interface IMetadataEditor
    {
        bool HasMetadata { get; }

        object Metadata { get; }
    }
}
