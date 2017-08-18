using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Values;

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
        /// نام وضعیت ثبتی مستند اداری
        /// </summary>
        [Display(Name = FieldNames.StatusField)]
        public string StatusName { get; set; }

        /// <summary>
        /// مجموعه ای از اقدامات انجام شده روی مستند اداری
        /// </summary>
        public IList<DocumentActionViewModel> Actions { get; protected set; }

        private static string GenerateNumber()
        {
            return Guid.NewGuid()
                .ToString()
                .Replace("{", String.Empty)
                .Replace("}", String.Empty)
                .Replace("-", String.Empty)
                .Substring(0, 8);
        }
    }
}
