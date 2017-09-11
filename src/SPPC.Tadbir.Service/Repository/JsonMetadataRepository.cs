using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Metadata.Workflow;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای را با استفاده از فایل داده ای پیاده سازی می کند
    /// </summary>
    public class JsonMetadataRepository : IMetadataRepository
    {
        /// <summary>
        /// اطلاعات فراداده ای مربوط به گردش های کاری تغییر وضعیت را خوانده و برمی گرداند
        /// </summary>
        /// <param name="documentType">نوع مستند اداری</param>
        /// <returns>اطلاعات فراداده ای گردش کار تغییر وضعیت</returns>
        public StateWorkflow GetStateWorkflow(string documentType)
        {
            var metadata = default(StateWorkflow);
            var assembly = typeof(StateWorkflow).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var allMetadata = Json.To<List<StateWorkflow>>(json);
                    metadata = allMetadata
                        .Where(swf => swf.DocumentType == DocumentTypeName.Transaction)
                        .FirstOrDefault();
                }
            }

            return metadata;
        }

        private static readonly string ResourceName = "SPPC.Tadbir.Metadata.JsonRepository.state-workflow.json";
    }
}
