using System;
using System.Collections.Generic;
using SPPC.Framework.Model.Workflow.Tracking;

namespace SPPC.Framework.NHibernate
{
    /// <summary>
    /// عملیات مرتبط با ذخیره وقایع گردش کاری در جریان ردگیری را تعریف می کند.
    /// </summary>
    public interface ITrackingRepository
    {
        /// <summary>
        /// مجموعه ای از رکوردهای ردگیری سفارشی را که با نام خاص طبقه بندی شده را برمی گرداند
        /// </summary>
        /// <param name="name">نام طبقه بندی مورد نیاز برای رکوردهای ردگیری</param>
        /// <returns>مجموعه رکوردهای ردگیری سفارشی با نام مورد نظر</returns>
        IList<CustomTrackingEvent> GetCustomEvents(string name);

        /// <summary>
        /// یک رکورد ردگیری گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="workflowEvent">رکورد ردگیری گردش کار</param>
        void SaveWorkflowEvent(WorkflowInstanceEvent workflowEvent);

        /// <summary>
        /// یک رکورد ردگیری فعالیت درون گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="activityEvent">رکورد ردگیری فعالیت درون گردش کار</param>
        void SaveActivityEvent(ActivityInstanceEvent activityEvent);

        /// <summary>
        /// یک رکورد ردگیری فعالیت درون گردش کار (شامل اطلاعات تکمیلی) را ذخیره می کند
        /// </summary>
        /// <param name="activityEvent">رکورد ردگیری فعالیت شامل اطلاعات تکمیلی</param>
        void SaveExtendedActivityEvent(ExtendedActivityEvent activityEvent);

        /// <summary>
        /// یک رکورد ردگیری وضعیت نشانه گذار درون گردش گار را ذخیره می کند.
        /// </summary>
        /// <param name="bookmarkEvent">رکورد ردگیری وضعیت نشانه گذار</param>
        void SaveBookmarkEvent(BookmarkResumptionEvent bookmarkEvent);

        /// <summary>
        /// یک رکورد ردگیری مربوط به وقایع خاص داخل گردش کار را ذخیره می کند
        /// </summary>
        /// <param name="customEvent">رکورد ردگیری مربوط به وقایع خاص</param>
        void SaveCustomEvent(CustomTrackingEvent customEvent);
    }
}
