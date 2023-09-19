using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.ProductScope
{
    public partial class BrandViewModel
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
