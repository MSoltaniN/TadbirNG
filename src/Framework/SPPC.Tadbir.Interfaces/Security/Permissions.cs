using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Utility class that wraps all current permissions with an easy-to-use design.
    /// </summary>
    public sealed class Permissions
    {
        private Permissions()
        {
        }

        /// <summary>
        /// Exposes all permissions currently applicable to managing a financial account.
        /// </summary>
        public static AccountPermission Account = AccountPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a detail account.
        /// </summary>
        public static DetailAccountPermission DetailAccount = DetailAccountPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a cost center.
        /// </summary>
        public static CostCenterPermission CostCenter = CostCenterPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a project.
        /// </summary>
        public static ProjectPermission Project = ProjectPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a financial voucher.
        /// </summary>
        public static VoucherPermission Voucher = VoucherPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing an application user.
        /// </summary>
        public static UserPermission User = UserPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing an application role.
        /// </summary>
        public static RolePermission Role = RolePermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a product inventory.
        /// </summary>
        public static ProductInventoryPermission ProductInventory = ProductInventoryPermission.Instance;

        /// <summary>
        /// Exposes all permissions currently applicable to managing a requisition voucher.
        /// </summary>
        public static RequisitionPermission Requisition = RequisitionPermission.Instance;
    }
}
