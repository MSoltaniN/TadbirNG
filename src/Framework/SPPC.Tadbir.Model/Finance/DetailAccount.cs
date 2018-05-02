using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class DetailAccount
    {
        /// <summary>
        /// مجموعه ای از تفصیلی های شناور زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<DetailAccount> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }
    }
}
