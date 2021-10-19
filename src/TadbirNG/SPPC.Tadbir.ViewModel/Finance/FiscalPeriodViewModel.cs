using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class FiscalPeriodViewModel : ViewModelBase
    {
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// شناسه دیتابیسی شرکت که این دوره مالی در آن تعریف شده است
        /// </summary>
        public int CompanyId { get; set; }
    }
}
