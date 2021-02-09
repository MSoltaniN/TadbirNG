using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel
{
    public interface IVoucherLineDetailView
    {
        DateTime VoucherDate { get; set; }

        int VoucherNo { get; set; }
    }
}
