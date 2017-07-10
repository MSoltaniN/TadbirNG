using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using SPPC.Framework.Mapper;
using SPPC.Framework.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    public class WorkflowRepository : IWorkflowRepository
    {
        public WorkflowRepository(IDomainMapper mapper, ITrackingRepository trackingRepository)
        {
            _mapper = mapper;
            _trackingRepository = trackingRepository;
        }

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
