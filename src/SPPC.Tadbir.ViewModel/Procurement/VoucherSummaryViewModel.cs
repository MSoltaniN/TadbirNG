using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    /// <summary>
    /// TODO: Add description
    /// </summary>
    public class VoucherSummaryViewModel
    {
        /// <summary>
        /// TODO: Add description
        /// </summary>
        public VoucherSummaryViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string RequesterName { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string RequesterUnitName { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string DocumentStatusName { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string DocumentOperationalStatus { get; set; }
    }
}
