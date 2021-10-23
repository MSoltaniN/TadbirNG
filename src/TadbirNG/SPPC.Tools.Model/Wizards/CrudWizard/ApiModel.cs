using System.Configuration;

namespace SPPC.Tools.Model
{
    public class ApiModel
    {
        public ApiModel()
        {
            OutputPath = ConfigurationManager.AppSettings["ApiFolder"];
        }
        
        public string EntityName { get; set; }

        public string OutputPath { get; set; }
    }
}
