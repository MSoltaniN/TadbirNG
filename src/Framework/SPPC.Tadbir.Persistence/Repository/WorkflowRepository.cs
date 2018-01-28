using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;
using SPPC.Workflow.Persistence;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مرتبط با خواندن اطلاعات گردش های کاری از دیتابیس را پیاده سازی می کند.
    /// </summary>
    public class WorkflowRepository : IWorkflowRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">پیاده سازی جاری برای نگاشت اطلاعات مدل های اطلاعاتی</param>
        /// <param name="trackingRepository">پیاده سازی جاری برای خواندن اطلاعات ردگیری گردش های کاری از محل ذخیره</param>
        public WorkflowRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, ITrackingRepository trackingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trackingRepository = trackingRepository;
        }

        /// <summary>
        /// اطلاعات گردش های کاری در حال اجرا در برنامه را از محل ذخیره می خواند
        /// </summary>
        /// <returns>گردش های کاری در حال اجرا</returns>
        public IList<WorkflowInstanceViewModel> GetRunningWorkflows()
        {
            var runningWorkflows = new List<WorkflowInstanceViewModel>();
            var groupedByType = _trackingRepository
                .GetCustomEvents(WorkflowEvent.StateChanged)
                .Select(evt => DeserializeData<string, object>(evt.SerializedData))
                .Select(data => _mapper.Map<WorkflowInstanceViewModel>(data))
                .GroupBy(inst => inst.DocumentType);
            foreach (var group in groupedByType)
            {
                var groupedById = group
                    .ToArray()
                    .GroupBy(inst => inst.DocumentId);
                foreach (var innerGroup in groupedById)
                {
                    var latest = innerGroup
                        .OrderByDescending(inst => inst.LastActionDate)
                        .First();
                    if (latest.State != DocumentStatusName.Approved)
                    {
                        latest.LastActor = GetUserName(Int32.Parse(latest.LastActor ?? "0"));
                        runningWorkflows.Add(latest);
                    }
                }
            }

            return runningWorkflows;
        }

        /// <summary>
        /// اطلاعات نمونه گردش کار در حال اجرا برای یک مستند را از محل ذخیره می خواند
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی یک مستند موجود</param>
        /// <param name="documentType">نوع مستند مورد نظر</param>
        /// <returns></returns>
        public WorkflowInstanceViewModel GetRunningInstance(int documentId, string documentType)
        {
            var instance = GetRunningWorkflows()
                .Where(inst => inst.DocumentId == documentId && inst.DocumentType == documentType)
                .FirstOrDefault();
            return instance;
        }

        private static IDictionary<TKey, TValue> DeserializeData<TKey, TValue>(string serializedData)
        {
            var data = new Dictionary<TKey, TValue>();
            if (!String.IsNullOrWhiteSpace(serializedData))
            {
                var serializer = new DataContractSerializer(typeof(IDictionary<TKey, TValue>));
                var settings = new XmlReaderSettings()
                {
                    IgnoreProcessingInstructions = true,
                    MaxCharactersInDocument = 0L
                };

                using (var dataReader = new StringReader(serializedData))
                {
                    using (var xmlReader = XmlReader.Create(dataReader, settings))
                    {
                        data = (Dictionary<TKey, TValue>)serializer.ReadObject(xmlReader, true);
                    }
                }
            }

            return data;
        }

        private string GetUserName(int userId)
        {
            string fullName = String.Empty;
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository.GetByID(userId);
            if (user != null)
            {
                fullName = String.Format("{0} {1}", user.Person.FirstName, user.Person.LastName);
            }

            return fullName;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private ITrackingRepository _trackingRepository;
    }
}
