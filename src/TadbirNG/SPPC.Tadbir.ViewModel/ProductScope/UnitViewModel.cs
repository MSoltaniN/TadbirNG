using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.ProductScope
{
    /// <summary>
    /// اطلاعات یک واحد را نگهدارری می کند
    /// </summary>
    public partial class UnitViewModel : ViewModelBase
    {
        /// <summary>
        /// شناسه یکتای شعبه سازمانی که برند در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///نام شعبه ای که برند در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }
    }
}
