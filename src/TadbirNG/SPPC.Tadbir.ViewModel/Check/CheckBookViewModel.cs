using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Check
{
    public partial class CheckBookViewModel
    {
        /// <summary>
        /// شناسه یکتای شعبه سازمانی که دسته چک در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///نام شعبه ای که دسته چک در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب مورد استفاده در این دسته چک 
        /// </summary>
        public virtual int? DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int? ProjectId { get; set; }

        /// <summary>
        /// مشخص می کند که دسته چک بعد از این دسته چک وجود دارد یا نه
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// مشخص می کند که دسته چک قبل از این دسته چک وجود دارد یا نه
        /// </summary>
        public bool HasPrevious { get; set; }
    }
}
