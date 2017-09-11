using System;
using System.Collections.Generic;
using BabakSoft.Platform.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات موجودی های کالا در انبار را پیاده سازی می کند.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public InventoryService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// مجموعه ای از اطلاعات موجودی های کالا در یک دوره مالی و یک شعبه خاص را برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        public IEnumerable<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId)
        {
            var inventories = _apiClient.Get<IEnumerable<ProductInventoryViewModel>>(
                InventoryApi.FiscalPeriodBranchInventories, fpId, branchId);
            return inventories;
        }

        /// <summary>
        /// اطلاعات نمایشی موجودی یک کالا در یک انبار را برمی گرداند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی موجودی یک کالا در انبار</param>
        /// <returns>اطلاعات نمایشی موجودی کالا در انبار</returns>
        public ProductInventoryViewModel GetProductInventory(int id)
        {
            var inventory = _apiClient.Get<ProductInventoryViewModel>(InventoryApi.Inventory, id);
            return inventory;
        }

        /// <summary>
        /// موجودی یک کالا در انبار را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="inventory">اطلاعات موجودی کالا در انبار</param>
        public void SaveProductInventory(ProductInventoryViewModel inventory)
        {
            Verify.ArgumentNotNull(inventory, "inventory");
            if (inventory.Id == 0)
            {
                _apiClient.Insert(inventory, InventoryApi.Inventories);
            }
            else
            {
                _apiClient.Update(inventory, InventoryApi.Inventory, inventory.Id);
            }
        }

        /// <summary>
        /// اطلاعات موجودی کالا را حذف می کند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی موجودی کالا در انبار</param>
        public void DeleteProductInventory(int id)
        {
            _apiClient.Delete(InventoryApi.Inventory, id);
        }

        private IApiClient _apiClient;
    }
}
