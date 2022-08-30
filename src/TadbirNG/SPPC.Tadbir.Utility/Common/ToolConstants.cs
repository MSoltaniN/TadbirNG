using System;

namespace SPPC.Tadbir.Utility
{
    public class ToolConstants
    {
        public const string GunzipTemplate = "gzip -d {0}";
        public const string GzipTemplate = "gzip {0}";
        public const string UntarTemplate = "tar -xf {0}";
        public const string TarTemplate = "tar -cf {0} {1}";
        public const string GitCommitTemplate = "git commit -a -m \"{0}\"";
        public const string GitPullCommand = "git pull --progress";
        public const string GitPushCommand = "git push --progress";
        public const string LayerTarFile = "layer.tar";
        public const string AppSettings = "appSettings.json";
        public const string DevAppSettings = "appSettings.Development.json";
        public const string DockerRemoveImageCommand = "docker image rm -f {0}";
        public const string DockerRemoveVolumeCommand = "docker volume rm {0}";
    }
}
