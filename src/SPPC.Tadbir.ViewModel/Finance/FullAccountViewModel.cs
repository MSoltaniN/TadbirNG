using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class FullAccountViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullAccountViewModel"/> class.
        /// </summary>
        public FullAccountViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل مالی از بردار حساب
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب
        /// </summary>
        public int CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب
        /// </summary>
        public int ProjectId { get; set; }
    }
}
