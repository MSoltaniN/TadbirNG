using System;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class VoucherLineDetailViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VoucherNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VoucherReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CurrencyValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BranchId { get; set; }
    }
}
