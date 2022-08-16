using System.IO;
using System.Reflection;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Configuration
{
    public class SysParameterUtility
    {
        static SysParameterUtility()
        {
            _sysParams = LoadParameters();
        }

        public static SysParameters AllParameters
        {
            get { return _sysParams; }
        }

        public static string DockerHubHandle
        {
            get { return _sysParams?.Docker?.HubHandle; }
        }

        public static DockerServiceParameters LicenseServer
        {
            get { return _sysParams?.Docker?.License; }
        }

        public static DockerServiceParameters ApiServer
        {
            get { return _sysParams?.Docker?.Api; }
        }

        public static DockerServiceParameters DbServer
        {
            get { return _sysParams?.Docker?.Db; }
        }

        public static DockerServiceParameters WebApp
        {
            get { return _sysParams?.Docker?.App; }
        }

        public static string GetImageFullName(DockerServiceParameters serviceParams, string tag = null)
        {
            return $"{DockerHubHandle}/{serviceParams.ImageName}:{tag ?? serviceParams.Tag}";
        }

        private static SysParameters LoadParameters()
        {
            var sysParams = default(SysParameters);
            var toolsAssembly = Assembly.GetAssembly(typeof(SysParameterUtility));
            if (toolsAssembly != null)
            {
                using var reader = new StreamReader(
                    toolsAssembly.GetManifestResourceStream(_sysParamsUri));
                string jsonConfig = reader.ReadToEnd();
                sysParams = JsonHelper.To<SysParameters>(jsonConfig);
            }

            return sysParams;
        }

        private const string _sysParamsUri = "SPPC.Tadbir.Configuration.sys-parameters.json";
        private static readonly SysParameters _sysParams;
    }
}
