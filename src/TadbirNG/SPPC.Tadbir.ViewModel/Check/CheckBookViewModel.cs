using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Check
{
    public partial class CheckBookViewModel
    {
        /// <summary>
        /// شناسه یکتای شعبه سازمانی که دسته چک در آن تعریف می شود
        /// </summary>
      public  int BranchId { get; set; }

        /// <summary>
        ///نام شعبه ای که دسته چک در آن تعریف شده است
        /// </summary>
      public  string BranchName { get; set; }
    }
}
