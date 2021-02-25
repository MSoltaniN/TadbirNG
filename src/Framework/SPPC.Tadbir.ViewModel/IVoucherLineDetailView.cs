using System;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    ///
    /// </summary>
    public interface IVoucherLineDetailView
    {
        /// <summary>
        ///
        /// </summary>
        DateTime VoucherDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        int VoucherNo { get; set; }
    }
}
