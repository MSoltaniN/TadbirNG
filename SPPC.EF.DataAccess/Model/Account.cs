﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Account
    {
        public Account()
        {
            FullAccount = new HashSet<FullAccount>();
            TransactionLine = new HashSet<TransactionLine>();
        }

        [Key]
        public int AccountId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Branch Branch { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public ICollection<FullAccount> FullAccount { get; set; }
        public ICollection<TransactionLine> TransactionLine { get; set; }
    }
}
