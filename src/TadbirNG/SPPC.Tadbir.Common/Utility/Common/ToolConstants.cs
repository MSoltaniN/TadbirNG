using System;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// مقادیر ثابت مورد استفاده در ابزارهای موجود را تعریف می کند
    /// </summary>
    public class ToolConstants
    {

        /// <summary>
        /// 
        /// </summary>
        public const string GunzipTemplate = "gzip -d {0}";

        /// <summary>
        /// 
        /// </summary>
        public const string GzipTemplate = "gzip {0}";

        /// <summary>
        /// 
        /// </summary>
        public const string UntarTemplate = "tar -xf {0}";

        /// <summary>
        /// 
        /// </summary>
        public const string TarTemplate = "tar -cf {0} {1}";

        /// <summary>
        /// 
        /// </summary>
        public const string GitCommitTemplate = "git commit -a -m \"{0}\"";

        /// <summary>
        /// 
        /// </summary>
        public const string GitPullCommand = "git pull --progress";

        /// <summary>
        /// 
        /// </summary>
        public const string GitPushCommand = "git push --progress";

        /// <summary>
        /// 
        /// </summary>
        public const string LayerTarFile = "layer.tar";

        /// <summary>
        /// 
        /// </summary>
        public const string AppSettings = "appSettings.json";

        /// <summary>
        /// 
        /// </summary>
        public const string DevAppSettings = "appSettings.Development.json";

        /// <summary>
        /// 
        /// </summary>
        public const string DockerRemoveImageCommand = "docker image rm -f {0}";

        /// <summary>
        /// 
        /// </summary>
        public const string DockerRemoveVolumeCommand = "docker volume rm {0}";
    }
}
