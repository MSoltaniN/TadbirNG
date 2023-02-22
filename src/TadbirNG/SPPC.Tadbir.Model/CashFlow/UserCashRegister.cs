using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Model.CashFlow
{
    public partial class UserCashRegister
    {
        /// <summary> 
        /// شناسه دیتابیسی صندوق این کاربر
        /// </summary> 
        
        public virtual int CashRegisterId { get; set; }
    }
}
