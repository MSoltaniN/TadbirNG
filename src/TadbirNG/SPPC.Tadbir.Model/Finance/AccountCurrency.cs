using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارتباط بین یک سرفصل حسابداری و یک ارز در یک شعبه را نگهداری می کند
    /// </summary>
    public class AccountCurrency : CoreEntity
    {
        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پول یا ارز مورد استفاده در این سرفصل حسابداری
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه که ارز در این شعبه انتخاب شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نمونه سرفصل حسابداری متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// نمونه ارز متناظر با شناسه دیتابسی موجود
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// نمونه شعبه متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public Branch Branch { get; set; }
    }
}
