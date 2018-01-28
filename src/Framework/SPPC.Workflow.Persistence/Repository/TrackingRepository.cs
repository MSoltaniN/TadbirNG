using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Model.Workflow.Tracking;
using SPPC.Framework.Persistence;

namespace SPPC.Workflow.Persistence
{
    /// <summary>
    /// عملیات مرتبط با ذخیره رکوردهای ردگیری گردش های کاری در دیتابیس را پیاده سازی می کند.
    /// </summary>
    public class TrackingRepository : ITrackingRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند.
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        public TrackingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// مجموعه ای از رکوردهای ردگیری سفارشی را که با نام خاص طبقه بندی شده را برمی گرداند
        /// </summary>
        /// <param name="name">نام طبقه بندی مورد نیاز برای رکوردهای ردگیری</param>
        /// <returns>مجموعه رکوردهای ردگیری سفارشی با نام مورد نظر</returns>
        public IList<CustomTrackingEvent> GetCustomEvents(string name)
        {
            var repository = _unitOfWork.GetRepository<CustomTrackingEvent>();
            var customEvents = repository
                .GetByCriteria(evt => evt.CustomRecordName == name)
                .OrderByDescending(evt => evt.TimeCreated)
                .ToList();
            _unitOfWork.Commit();
            return customEvents;
        }

        /// <summary>
        /// یک رکورد ردگیری گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="workflowEvent">رکورد ردگیری گردش کار</param>
        public void SaveWorkflowEvent(WorkflowInstanceEvent workflowEvent)
        {
            var repository = _unitOfWork.GetRepository<WorkflowInstanceEvent>();
            repository.Insert(workflowEvent);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// یک رکورد ردگیری فعالیت درون گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="activityEvent">رکورد ردگیری فعالیت درون گردش کار</param>
        public void SaveActivityEvent(ActivityInstanceEvent activityEvent)
        {
            var repository = _unitOfWork.GetRepository<ActivityInstanceEvent>();
            repository.Insert(activityEvent);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// یک رکورد ردگیری فعالیت درون گردش کار (شامل اطلاعات تکمیلی) را ذخیره می کند
        /// </summary>
        /// <param name="activityEvent">رکورد ردگیری فعالیت شامل اطلاعات تکمیلی</param>
        public void SaveExtendedActivityEvent(ExtendedActivityEvent activityEvent)
        {
            var repository = _unitOfWork.GetRepository<ExtendedActivityEvent>();
            repository.Insert(activityEvent);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// یک رکورد ردگیری وضعیت نشانه گذار درون گردش گار را ذخیره می کند.
        /// </summary>
        /// <param name="bookmarkEvent">رکورد ردگیری وضعیت نشانه گذار</param>
        public void SaveBookmarkEvent(BookmarkResumptionEvent bookmarkEvent)
        {
            var repository = _unitOfWork.GetRepository<BookmarkResumptionEvent>();
            repository.Insert(bookmarkEvent);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// یک رکورد ردگیری مربوط به وقایع خاص داخل گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="customEvent">رکورد ردگیری مربوط به وقایع خاص</param>
        public void SaveCustomEvent(CustomTrackingEvent customEvent)
        {
            var repository = _unitOfWork.GetRepository<CustomTrackingEvent>();
            repository.Insert(customEvent);
            _unitOfWork.Commit();
        }

        private IUnitOfWork _unitOfWork;
    }
}
