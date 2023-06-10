using System;

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
    }
}
