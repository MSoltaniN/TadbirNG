using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class DocumentViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی نوع مستند اداری
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی مستند اداری
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// مجموعه ای از اقدامات انجام شده روی مستند اداری
        /// </summary>
        public IList<DocumentActionViewModel> Actions { get; protected set; }
    }
}
