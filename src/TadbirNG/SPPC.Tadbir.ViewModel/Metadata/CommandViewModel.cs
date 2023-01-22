using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class CommandViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دسترسی مورد نیاز برای مشاهده و اجرای این دستور
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دستور والد برای این دستور در ساختار درختی دستورات
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// مجموعه ای از دستورات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<CommandViewModel> Children { get; protected set; }

        /// <summary>
        /// وضعیت اطلاعات این دستور در دیتابیس
        /// </summary>
        public RecordState State { get; set; }

        /// <summary>
        /// اطلاعات آبجکت جاری را به صورت رشته متنی برمی گرداند
        /// </summary>
        /// <returns>اطلاعات آبجکت جاری به صورت رشته متنی</returns>
        public override string ToString()
        {
            return $"{Title} (Children : {Children.Count}, {State})";
        }
    }
}
