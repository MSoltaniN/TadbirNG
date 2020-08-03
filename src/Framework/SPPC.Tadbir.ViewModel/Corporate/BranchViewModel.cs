namespace SPPC.Tadbir.ViewModel.Corporate
{
    public partial class BranchViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company related to this branch.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

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
