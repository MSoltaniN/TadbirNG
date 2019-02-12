using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class AccountCollectionAccount : IFiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مجموعه حساب در این ارتباط
        /// </summary>
        public virtual int CollectionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی در این ارتباط
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه در این ارتباط
        /// </summary>
        public int BranchId { get; set; }
    }
}
