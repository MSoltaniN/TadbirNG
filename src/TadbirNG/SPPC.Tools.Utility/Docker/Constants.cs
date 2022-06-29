using System;

namespace SPPC.Tools.Utility
{
    internal class Constants
    {
        internal const string Root = @"D:\Temp\__Test2__";
        internal const string LicenseServerFile = "license-server.tar.gz";
        internal const string ApiServerFile = "api-server.tar.gz";
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
