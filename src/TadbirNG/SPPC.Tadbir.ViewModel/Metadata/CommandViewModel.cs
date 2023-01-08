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

        public override string ToString()
        {
            return $"{Title} (ChildrenCount : {Children.Count})";
        }
    }
}
