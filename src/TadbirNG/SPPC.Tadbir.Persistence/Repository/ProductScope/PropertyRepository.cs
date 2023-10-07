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
    /// عملیات مورد نیاز برای مدیریت ویژگی ها را پیاده سازی می کند
    /// </summary>
    public class PropertyRepository : EntityLoggingRepository<Property, PropertyViewModel>, IPropertyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public PropertyRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<PropertyViewModel>> GetPropertiesAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var properties = new List<PropertyViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<Property>(ViewId.Property);
                properties = await query
                    .Select(item => Mapper.Map<PropertyViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<PropertyViewModel>(properties, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<PropertyViewModel> GetPropertyAsync(int propertyId)
        {
            PropertyViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Property>();
            var property = await repository.GetByIDAsync(propertyId);
            if (property != null)
            {
                item = Mapper.Map<PropertyViewModel>(property);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<PropertyViewModel> SavePropertyAsync(PropertyViewModel property)
        {
            Verify.ArgumentNotNull(property, nameof(property));
            Property propertyModel;
            var repository = UnitOfWork.GetAsyncRepository<Property>();
            if (property.Id == 0)
            {
                propertyModel = Mapper.Map<Property>(property);
                await InsertAsync(repository, propertyModel);
            }
            else
            {
                propertyModel = await repository.GetByIDAsync(property.Id);
                if (propertyModel != null)
                {
                    await UpdateAsync(repository, propertyModel, property);
                }
            }

            return Mapper.Map<PropertyViewModel>(propertyModel);
        }

        /// <inheritdoc/>
        public async Task DeletePropertyAsync(int propertyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Property>();
            var property = await repository.GetByIDAsync(propertyId);
            if (property != null)
            {
                await DeleteAsync(repository, property);
            }
        }

        /// <inheritdoc/>
        public async Task DeletePropertiesAsync(IList<int> propertyIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Property>();
            foreach (int propertyId in propertyIds)
            {
                var property = await repository.GetByIDAsync(propertyId);
                if (property != null)
                {
                    await DeleteNoLogAsync(repository, property);
                }
            }

            await OnEntityGroupDeleted(propertyIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Property; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(PropertyViewModel propertyViewModel, Property property)
        {
            property.Name = propertyViewModel.Name;
            property.EnName = propertyViewModel.EnName;
            property.Description = propertyViewModel.Description;
            property.Type = propertyViewModel.Type;
            property.Prefix = propertyViewModel.Prefix;
            property.Suffix = propertyViewModel.Suffix;
            property.IsActive = propertyViewModel.IsActive;
        }

        /// <inheritdoc/>
        protected override string GetState(Property entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9} , {10} : {11} , {12} : {13}",
                    AppStrings.Name, entity.Name, AppStrings.EnName, entity.EnName,
                    AppStrings.Description, entity.Description, AppStrings.Type, entity.Type,
                    AppStrings.Prefix, entity.Prefix, AppStrings.Suffix, entity.Suffix,
                    AppStrings.IsActive, entity.IsActive)
                : null;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
