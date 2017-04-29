using System;
using System.Activities;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// این فعالیت با استفاده از امکان نشانه گذاری، اجرای فعالیت را تا زمان بدست آوردن وضعیت جدید متوقف می کند.
    /// </summary>
    /// <remarks>
    /// امکان نشانه گذاری نقطه اجرایی، پیاده سازی گردش کارهای ماشین حالت را ساده می کند.
    /// <para>معادل های فارسی به کار رفته :</para>
    /// <para>نشانه گذاری : Bookmarking | گردش کار : Workflow | ماشین حالت : State Machine
    /// </para>
    /// </remarks>
    public class WaitForStateActivity : NativeActivity<string>
    {
        /// <summary>
        /// نام نشانه گذار استفاده شده برای توقف و ادامه اجرای فعالیت
        /// </summary>
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        /// <summary>
        /// یک نشانه گذار با نام تنظیم شده ایجاد کرده و اجرای فعالیت را متوقف می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        protected override void Execute(NativeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            string name = BookmarkName.Get(context);
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("BookmarkName cannot be an Empty string.", "BookmarkName");
            }

            context.CreateBookmark(name, new BookmarkCallback(OnReceiveStatus));
        }

        // NativeActivity derived activities that do asynchronous operations by calling 
        // one of the CreateBookmark overloads defined on System.Activities.NativeActivityContext 
        // must override the CanInduceIdle property and return true.
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        private void OnReceiveStatus(NativeActivityContext context, Bookmark bookmark, object state)
        {
            Result.Set(context, Convert.ToString(state));
        }
    }
}
