namespace SPPC.Tadbir.ViewModel.Corporate
{
    public partial class BranchViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company related to this branch.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if this branch is accessible by a role.
        /// </summary>
        public bool IsAccessible { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// تعداد شعب سازمانی زیرمجموعه این مرکز هزینه در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }

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
