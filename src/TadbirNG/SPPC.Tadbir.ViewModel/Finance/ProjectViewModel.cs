﻿using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class ProjectViewModel : ViewModelBase, IFiscalEntity, ITreeEntityView, IBaseEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این پروژه در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این پروژه در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// تعداد پروژه های زیرمجموعه این پروژه در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیر فعال برای این سطر اطلاعاتی
        /// </summary>
        public string State { get; set; }
    }
}
