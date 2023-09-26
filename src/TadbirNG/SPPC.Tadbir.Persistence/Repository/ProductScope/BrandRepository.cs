using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.ProductScope;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت برندها را پیاده سازی می کند
    /// </summary>
    public class BrandRepository : EntityLoggingRepository<Brand, BrandViewModel>, IBrandRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public BrandRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<BrandViewModel>> GetBrandsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var brands = new List<BrandViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<Brand>(ViewId.Brand);
                brands = await query
                    .Select(item => Mapper.Map<BrandViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<BrandViewModel>(brands, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<BrandViewModel> GetBrandAsync(int brandId)
        {
            BrandViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Brand>();
            var brand = await repository.GetByIDAsync(brandId);
            if (brand != null)
            {
                item = Mapper.Map<BrandViewModel>(brand);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<BrandViewModel> SaveBrandAsync(BrandViewModel brand)
        {
            Verify.ArgumentNotNull(brand, nameof(brand));
            Brand brandModel;
            var repository = UnitOfWork.GetAsyncRepository<Brand>();
            if (brand.Id == 0)
            {
                brandModel = Mapper.Map<Brand>(brand);
                SetBaseEntityInfo(brandModel);
                await InsertAsync(repository, brandModel);
            }
            else
            {
                brandModel = await repository.GetByIDAsync(brand.Id);
                if (brandModel != null)
                {
                    await UpdateAsync(repository, brandModel, brand);
                }
            }

            return Mapper.Map<BrandViewModel>(brandModel);
        }

        /// <inheritdoc/>
        public async Task DeleteBrandAsync(int brandId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Brand>();
            var brand = await repository.GetByIDAsync(brandId);
            if (brand != null)
            {
                await DeleteAsync(repository, brand);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteBrandsAsync(IList<int> brandIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Brand>();
            foreach (int brandId in brandIds)
            {
                var brand = await repository.GetByIDAsync(brandId);
                if (brand != null)
                {
                    await DeleteNoLogAsync(repository, brand);
                }
            }

            await OnEntityGroupDeleted(brandIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Brand; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(BrandViewModel brandViewModel, Brand brand)
        {
            brand.Name = brandViewModel.Name;
            brand.EnName = brandViewModel.EnName;
            brand.Description = brandViewModel.Description;
            brand.SocialLink = brandViewModel.SocialLink;
            brand.Website = brandViewModel.Website;
            brand.MetaKeyword = brandViewModel.MetaKeyword;
            brand.IsActive = brandViewModel.IsActive;
        }

        /// <inheritdoc/>
        protected override string GetState(Brand entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9} , {10} : {11} , {12} : {13}",
                    AppStrings.Name, entity.Name, AppStrings.EnName, entity.EnName,
                    AppStrings.Description, entity.Description, AppStrings.SocialLink, entity.SocialLink,
                    AppStrings.Website, entity.Website, AppStrings.MetaKeyword,
                    entity.MetaKeyword, AppStrings.IsActive, entity.IsActive)
                : null;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
