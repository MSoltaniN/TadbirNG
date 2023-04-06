using SPPC.Tadbir.ViewModel.Finance;

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
        /// بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// مشخص می کند که دسته چک بعد از این دسته چک وجود دارد یا نه
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// مشخص می کند که دسته چک قبل از این دسته چک وجود دارد یا نه
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// تعداد برگه های دسته چک
        /// </summary>
        public int PageCount { get; set; }
    }
}
