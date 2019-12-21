using System;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// TODO: Add comment
    /// </summary>
    public class VoucherLineDetailViewModel
    {
        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public int VoucherNo { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string VoucherReference { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public decimal CurrencyValue { get; set; }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        public int BranchId { get; set; }
    }
}
