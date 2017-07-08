using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using AutoMapper;
using SPPC.Framework.Model.Workflow.Tracking;

namespace SPPC.Framework.Mapper
{
    /// <summary>
    /// Provides support for mappings between framework model and view model classes using the AutoMapper
    /// object mapping library.
    /// </summary>
    public partial class DomainMapper : IDomainMapper
    {
        static DomainMapper()
        {
            _configuration = new MapperConfiguration(config => RegisterMappings(config));
            _autoMapper = _configuration.CreateMapper();
        }

        /// <summary>
        /// Gets an object used for mapping configuration.
        /// </summary>
        public object Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        /// Maps source object to another object having a potentially different type.
        /// </summary>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <param name="source">Source object that should be mapped</param>
        /// <returns>The target object mapped from the source object</returns>
        public T Map<T>(object source)
        {
            return _autoMapper.Map<T>(source);
        }

        private static void RegisterMappings(IMapperConfigurationExpression mapperConfig)
        {
            MapTrackingTypes(mapperConfig);
        }

        private static void MapTrackingTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<WorkflowInstanceRecord, WorkflowInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityDefinition, opts => opts.MapFrom(src => src.ActivityDefinitionId));
            mapperConfig.CreateMap<WorkflowInstanceUnhandledExceptionRecord, WorkflowInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityDefinition, opts => opts.MapFrom(src => src.ActivityDefinitionId))
                .ForMember(dest => dest.ExceptionDetails, opts => opts.MapFrom(src => src.UnhandledException.ToString()));
            mapperConfig.CreateMap<WorkflowInstanceTerminatedRecord, WorkflowInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityDefinition, opts => opts.MapFrom(src => src.ActivityDefinitionId));
            mapperConfig.CreateMap<WorkflowInstanceAbortedRecord, WorkflowInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityDefinition, opts => opts.MapFrom(src => src.ActivityDefinitionId));
            mapperConfig.CreateMap<WorkflowInstanceSuspendedRecord, WorkflowInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityDefinition, opts => opts.MapFrom(src => src.ActivityDefinitionId));

            mapperConfig.CreateMap<ActivityStateRecord, ActivityInstanceEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.SerializedArguments, opts => opts.MapFrom(src => SerializeData(src.Arguments)))
                .ForMember(dest => dest.SerializedVariables, opts => opts.MapFrom(src => SerializeData(src.Variables)))
                .ForMember(dest => dest.ActivityRecordType, opts => opts.UseValue("ActivityState"))
                .ForMember(dest => dest.ActivityName, opts => opts.MapFrom(src => src.Activity.Name))
                .ForMember(dest => dest.ActivityId, opts => opts.MapFrom(src => src.Activity.Id))
                .ForMember(dest => dest.ActivityInstanceId, opts => opts.MapFrom(src => src.Activity.InstanceId))
                .ForMember(dest => dest.ActivityType, opts => opts.MapFrom(src => src.Activity.TypeName));

            mapperConfig.CreateMap<ActivityScheduledRecord, ExtendedActivityEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityRecordType, opts => opts.UseValue("ActivityScheduled"))
                .ForMember(dest => dest.ActivityName, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.Name))
                .ForMember(dest => dest.ActivityId, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.Id))
                .ForMember(dest => dest.ActivityInstanceId, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.InstanceId))
                .ForMember(dest => dest.ActivityType, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.TypeName))
                .ForMember(dest => dest.ChildActivityName, opts => opts.MapFrom(src => src.Child.Name))
                .ForMember(dest => dest.ChildActivityId, opts => opts.MapFrom(src => src.Child.Id))
                .ForMember(dest => dest.ChildActivityInstanceId, opts => opts.MapFrom(src => src.Child.InstanceId))
                .ForMember(dest => dest.ChildActivityType, opts => opts.MapFrom(src => src.Child.TypeName));

            mapperConfig.CreateMap<CancelRequestedRecord, ExtendedActivityEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityRecordType, opts => opts.UseValue("CancelRequested"))
                .ForMember(dest => dest.ActivityName, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.Name))
                .ForMember(dest => dest.ActivityId, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.Id))
                .ForMember(dest => dest.ActivityInstanceId, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.InstanceId))
                .ForMember(dest => dest.ActivityType, opts => opts.MapFrom(src => src.Activity == null ? null : src.Activity.TypeName))
                .ForMember(dest => dest.ChildActivityName, opts => opts.MapFrom(src => src.Child.Name))
                .ForMember(dest => dest.ChildActivityId, opts => opts.MapFrom(src => src.Child.Id))
                .ForMember(dest => dest.ChildActivityInstanceId, opts => opts.MapFrom(src => src.Child.InstanceId))
                .ForMember(dest => dest.ChildActivityType, opts => opts.MapFrom(src => src.Child.TypeName));

            mapperConfig.CreateMap<FaultPropagationRecord, ExtendedActivityEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.ActivityRecordType, opts => opts.UseValue("FaultPropagation"))
                .ForMember(dest => dest.ActivityName, opts => opts.MapFrom(src => src.FaultSource.Name))
                .ForMember(dest => dest.ActivityId, opts => opts.MapFrom(src => src.FaultSource.Id))
                .ForMember(dest => dest.ActivityInstanceId, opts => opts.MapFrom(src => src.FaultSource.InstanceId))
                .ForMember(dest => dest.ActivityType, opts => opts.MapFrom(src => src.FaultSource.TypeName))
                .ForMember(dest => dest.FaultDetails, opts => opts.MapFrom(src => src.Fault.ToString()))
                .ForMember(dest => dest.FaultHandlerActivityName, opts => opts.MapFrom(src => src.FaultHandler == null ? null : src.FaultHandler.Name))
                .ForMember(dest => dest.FaultHandlerActivityId, opts => opts.MapFrom(src => src.FaultHandler == null ? null : src.FaultHandler.Id))
                .ForMember(dest => dest.FaultHandlerActivityInstanceId, opts => opts.MapFrom(src => src.FaultHandler == null ? null : src.FaultHandler.InstanceId))
                .ForMember(dest => dest.FaultHandlerActivityType, opts => opts.MapFrom(src => src.FaultHandler == null ? null : src.FaultHandler.TypeName));

            mapperConfig.CreateMap<BookmarkResumptionRecord, BookmarkResumptionEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.OwnerActivityName, opts => opts.MapFrom(src => src.Owner.Name))
                .ForMember(dest => dest.OwnerActivityId, opts => opts.MapFrom(src => src.Owner.Id))
                .ForMember(dest => dest.OwnerActivityInstanceId, opts => opts.MapFrom(src => src.Owner.InstanceId))
                .ForMember(dest => dest.OwnerActivityType, opts => opts.MapFrom(src => src.Owner.TypeName));

            mapperConfig.CreateMap<CustomTrackingRecord, CustomTrackingEvent>()
                .BeforeMap((src, target) => MapCommonTrackingValues(src, target))
                .ForMember(dest => dest.SerializedData, opts => opts.MapFrom(src => SerializeData(src.Data)))
                .ForMember(dest => dest.CustomRecordName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ActivityName, opts => opts.MapFrom(src => src.Activity.Name))
                .ForMember(dest => dest.ActivityId, opts => opts.MapFrom(src => src.Activity.Id))
                .ForMember(dest => dest.ActivityInstanceId, opts => opts.MapFrom(src => src.Activity.InstanceId))
                .ForMember(dest => dest.ActivityType, opts => opts.MapFrom(src => src.Activity.TypeName));
        }

        private static void MapCommonTrackingValues(TrackingRecord record, WorkflowInstanceEvent workflowEvent)
        {
            workflowEvent.TraceLevelId = (byte)record.Level;
            workflowEvent.SerializedAnnotations = SerializeData(record.Annotations);
            workflowEvent.TimeCreated = record.EventTime;
            workflowEvent.WorkflowInstanceId = record.InstanceId;
        }

        private static void MapCommonTrackingValues(TrackingRecord record, ActivityInstanceEvent activityEvent)
        {
            activityEvent.TraceLevelId = (byte)record.Level;
            activityEvent.SerializedAnnotations = SerializeData(record.Annotations);
            activityEvent.TimeCreated = record.EventTime;
            activityEvent.WorkflowInstanceId = record.InstanceId;
        }

        private static void MapCommonTrackingValues(TrackingRecord record, ExtendedActivityEvent activityEvent)
        {
            activityEvent.TraceLevelId = (byte)record.Level;
            activityEvent.SerializedAnnotations = SerializeData(record.Annotations);
            activityEvent.TimeCreated = record.EventTime;
            activityEvent.WorkflowInstanceId = record.InstanceId;
        }

        private static void MapCommonTrackingValues(TrackingRecord record, BookmarkResumptionEvent activityEvent)
        {
            activityEvent.TraceLevelId = (byte)record.Level;
            activityEvent.SerializedAnnotations = SerializeData(record.Annotations);
            activityEvent.TimeCreated = record.EventTime;
            activityEvent.WorkflowInstanceId = record.InstanceId;
        }

        private static void MapCommonTrackingValues(TrackingRecord record, CustomTrackingEvent activityEvent)
        {
            activityEvent.TraceLevelId = (byte)record.Level;
            activityEvent.SerializedAnnotations = SerializeData(record.Annotations);
            activityEvent.TimeCreated = record.EventTime;
            activityEvent.WorkflowInstanceId = record.InstanceId;
        }

        private static string SerializeData<TKey, TValue>(IDictionary<TKey, TValue> data)
        {
            string serialized = String.Empty;
            if (data.Count > 0)
            {
                var serializer = new NetDataContractSerializer();
                var builder = new StringBuilder();
                var settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
                using (XmlWriter writer = XmlWriter.Create(builder, settings))
                {
                    serializer.WriteObject(writer, data);
                    writer.Flush();
                    serialized = builder.ToString();
                }
            }

            return serialized;
        }

        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
