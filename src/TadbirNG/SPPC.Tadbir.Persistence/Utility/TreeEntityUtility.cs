using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مورد نیاز برای موجودیت های درختی را به صورت مرکزی پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TModel">نوع مدل اطلاعاتی موجودیت درختی</typeparam>
    /// <typeparam name="TViewModel">نوع مدل نمایشی موجودیت درختی</typeparam>
    public class TreeEntityUtility<TModel, TViewModel>
        where TModel : TreeEntity
        where TViewModel : class, IFiscalEntity, ITreeEntityView, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="treeConfig">پیکربندی موجود برای ساختار درختی موجودیت مورد نظر</param>
        public TreeEntityUtility(IRepositoryContext context, ViewTreeConfig treeConfig)
        {
            _unitOfWork = context.UnitOfWork;
            _userContext = context.UserContext;
            _treeConfig = treeConfig;
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل موجودیت درختی با شناسه داده شده را برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی یکی از رکوردهای موجود</param>
        /// <returns>اگر رکوردی با شناسه داده شده وجود نداشته باشد مقدار خالی
        /// و در غیر این صورت کد کامل را برمی گرداند</returns>
        public async Task<string> GetItemFullCodeAsync(int itemId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            var item = await repository.GetByIDAsync(itemId);
            if (item == null)
            {
                return String.Empty;
            }

            return item.FullCode;
        }

        /// <summary>
        /// به روش آسنکرون، برای رکورد والد مشخص شده رکورد زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی رکورد والد : اگر مقدار نداشته باشد رکورد جدیدی
        /// در سطح اول پیشنهاد می شود</param>
        /// <returns>مدل نمایشی رکورد پیشنهادی</returns>
        public async Task<TViewModel> GetNewChildItemAsync(int? parentId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            var parent = await repository.GetByIDAsync(parentId ?? 0);
            if (parentId > 0 && parent == null)
            {
                return null;
            }

            if (parent != null && parent.Level + 1 == _treeConfig.MaxDepth)
            {
                return new TViewModel() { Level = -1 };
            }

            var childrenCodes = await GetChildrenCodesAsync(parentId);
            string newCode = NumericText.GetNewCodeValue(childrenCodes);
            return GetNewChildItem(parent, newCode);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا رکورد درختی انتخاب شده دارای زیرمجموعه است یا نه
        /// </summary>
        /// <param name="itemId">شناسه یکتای یکی از رکوردهای موجود</param>
        /// <returns>در حالتی که رکورد مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasChildrenAsync(int itemId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            int childCount = await repository
                .GetEntityQuery()
                .CountAsync(item => item.ParentId == itemId);
            return childCount > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد موجودیت درختی مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="itemView">مدل نمایشی موجودیت درختی مورد نظر</param>
        /// <returns>اگر کد موجودیت درختی تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateFullCodeAsync(TViewModel itemView)
        {
            Verify.ArgumentNotNull(itemView, nameof(itemView));
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            int count = await repository
                .GetCountByCriteriaAsync(item => item.Id != ((IFiscalEntity)itemView).Id
                    && item.FullCode == itemView.FullCode);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام موجودیت درختی مورد نظر بین اقلام همسطح با والد یکسان تکراری است یا نه
        /// </summary>
        /// <param name="itemView">مدل نمایشی موجودیت درختی مورد نظر</param>
        /// <returns>اگر نام موجودیت درختی تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateNameAsync(TViewModel itemView)
        {
            Verify.ArgumentNotNull(itemView, nameof(itemView));
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            int count = await repository
                .GetCountByCriteriaAsync(item => item.Id != ((IFiscalEntity)itemView).Id
                    && item.ParentId == itemView.ParentId
                    && item.Name == itemView.Name);
            return count > 0;
        }

        private async Task<IList<string>> GetChildrenCodesAsync(int? parentId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TModel>();
            return await repository
                .GetEntityQuery()
                .Where(prj => prj.ParentId == parentId)
                .Select(prj => prj.Code)
                .ToListAsync();
        }

        private TViewModel GetNewChildItem(TModel parent, string newCode)
        {
            var child = new TViewModel()
            {
                Code = newCode,
                ParentId = parent?.Id,
                FiscalPeriodId = _userContext.FiscalPeriodId,
                BranchId = _userContext.BranchId
            };
            child.FullCode = (parent != null)
                ? parent.FullCode + child.Code
                : child.Code;
            if (parent != null)
            {
                child.Level = (short)((parent.Level + 1 < _treeConfig.MaxDepth)
                    ? parent.Level + 1
                    : -1);
            }

            return child;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly ViewTreeConfig _treeConfig;
        private readonly UserContextViewModel _userContext;
    }
}
