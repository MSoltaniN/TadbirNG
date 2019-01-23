using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountCollectionCategoryViewModel
    {
        /// <summary>
        /// لیستی از مجموعه حسابهای این گروه
        /// </summary>
        public IList<AccountCollectionViewModel> AccountCollections { get; set; }
    }
}
