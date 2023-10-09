using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.ProductScope
{
    /// <summary>
    /// اطلاعات یک واحد را نگهدارری می کند
    /// </summary>
    public partial class PropertyViewModel : ViewModelBase
    {
        /// <summary>
        /// شناسه یکتای شعبه سازمانی که ویژگی در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///نام شعبه ای که ویژگی در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }
    }
}
