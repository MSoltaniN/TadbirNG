using System;

namespace SPPC.Tools.Utility
{
    internal class ToolConstants
    {
        internal const string GunzipTemplate = "gzip -d {0}";
        internal const string GzipTemplate = "gzip {0}";
        internal const string UntarTemplate = "tar -xf {0}";
        internal const string TarTemplate = "tar -cf {0} {1}";
        internal const string LayerTarFile = "layer.tar";
        internal const string AppSettings = "appSettings.json";
        internal const string OldAppSettings = "old_appSettings.json";
        internal const string DevAppSettings = "appSettings.Development.json";
    }
}
