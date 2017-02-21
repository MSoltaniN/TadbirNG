using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Provides complete details for a financial account, including additional fields from related entities.
    /// </summary>
    public class AccountFullViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user-friendly code of the account
        /// </summary>
        [Display(Name = FieldNames.AccountCodeField)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the account
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this account
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of fiscal period in which this account is defined.
        /// </summary>
        [Display(Name = FieldNames.FiscalPeriodEntity)]
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// Gets or sets the name of company branch in which the fiscal period of this account is defined.
        /// </summary>
        [Display(Name = FieldNames.BranchEntity)]
        public string FiscalPeriodBranchName { get; set; }

        /// <summary>
        /// Gets or sets the name of company in which the branch for this account is defined.
        /// </summary>
        [Display(Name = FieldNames.CompanyEntity)]
        public string FiscalPeriodBranchCompanyName { get; set; }
    }
}
