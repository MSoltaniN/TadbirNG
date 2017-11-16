using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت موجودی های کالا در انبار را تعریف می کند
    /// </summary>
    public interface IInventoryRepository
    {
        /// <summary>
        /// اطلاعات موجودی های کالا در انبار را در یک دوره مالی و یک شعبه خاص خوانده و بر می گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از اطلاعات نمایشی موجودی های کالا</returns>
        IList<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId);

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را خوانده و بر می گرداند
        /// </summary>
        /// <param name="inventoryId">شناسه دیتابیسی موجودی کالا در انبار</param>
        /// <returns>اطلاعات نمایشی موجودی کالا در انبار</returns>
        ProductInventoryViewModel GetProductInventory(int inventoryId);

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="inventory">اطلاعات نمایشی موجودی یک کالا در یک انبار</param>
        void SaveProductInventory(ProductInventoryViewModel inventory);

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را حذف می کند
        /// </summary>
        /// <param name="inventoryId">شناسه دیتابیسی موجودی کالا در انبار</param>
        void DeleteProductInventory(int inventoryId);
    }
}
