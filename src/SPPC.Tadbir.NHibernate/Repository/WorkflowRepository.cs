﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using SPPC.Framework.Mapper;
using SPPC.Framework.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات مرتبط با خواندن اطلاعات گردش های کاری از دیتابیس را پیاده سازی می کند.
    /// </summary>
    public class WorkflowRepository : IWorkflowRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="mapper">پیاده سازی جاری برای نگاشت اطلاعات مدل های اطلاعاتی</param>
        /// <param name="trackingRepository">پیاده سازی جاری برای خواندن اطلاعات ردگیری گردش های کاری از محل ذخیره</param>
        public WorkflowRepository(IDomainMapper mapper, ITrackingRepository trackingRepository)
        {
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
            var groupedRecords = _trackingRepository
                .GetCustomEvents(WorkflowEvent.StateChanged)
                .GroupBy(evt => evt.WorkflowInstanceId);
            foreach (var grouping in groupedRecords)
            {
                var lastEventData = grouping
                    .OrderByDescending(evt => evt.TimeCreated)
                    .Select(evt => evt.SerializedData)
                    .First();
                var eventData = DeserializeData<string, object>(lastEventData);
                if ((string)eventData["State"] != DocumentStatus.Approved)
                {
                    runningWorkflows.Add(_mapper.Map<WorkflowInstanceViewModel>(eventData));
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
                var serializer = new NetDataContractSerializer();
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

        private IDomainMapper _mapper;
        private ITrackingRepository _trackingRepository;
    }
}
