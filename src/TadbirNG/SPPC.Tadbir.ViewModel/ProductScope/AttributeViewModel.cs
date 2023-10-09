using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.ProductScope
{
    /// <summary>
    /// اطلاعات یک خصوصیت را نگهدارری می کند
    /// </summary>
    public partial class AttributeViewModel : ViewModelBase
    {
        /// <summary>
        /// شناسه یکتای شعبه سازمانی که خصوصیت در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///نام شعبه ای که خصوصیت در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }
    }
}
