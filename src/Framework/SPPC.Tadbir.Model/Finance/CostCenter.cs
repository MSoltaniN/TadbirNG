using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class CostCenter
    {
        /// <summary>
        /// مجموعه ای از مراکز هزینه زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<CostCenter> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }
    }
}
