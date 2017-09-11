using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات موجودی های کالا در انبار را تعریف می کند.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// مجموعه ای از اطلاعات موجودی های کالا در یک دوره مالی و یک شعبه خاص را برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        IEnumerable<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId);

        /// <summary>
        /// اطلاعات نمایشی موجودی یک کالا در یک انبار را برمی گرداند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی موجودی یک کالا در انبار</param>
        /// <returns>اطلاعات نمایشی موجودی کالا در انبار</returns>
        ProductInventoryViewModel GetProductInventory(int id);

        /// <summary>
        /// موجودی یک کالا در انبار را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="inventory">اطلاعات موجودی کالا در انبار</param>
        void SaveProductInventory(ProductInventoryViewModel inventory);

        /// <summary>
        /// اطلاعات موجودی کالا را حذف می کند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی موجودی کالا در انبار</param>
        void DeleteProductInventory(int id);
    }
}
