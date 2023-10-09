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
using Attribute = SPPC.Tadbir.Model.ProductScope.Attribute;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت خصوصیت ها را پیاده سازی می کند
    /// </summary>
    public class AttributeRepository : EntityLoggingRepository<Attribute, AttributeViewModel>, IAttributeRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public AttributeRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<AttributeViewModel>> GetAttributesAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var attributes = new List<AttributeViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<Attribute>(ViewId.Attribute);
                attributes = await query
                    .Select(item => Mapper.Map<AttributeViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<AttributeViewModel>(attributes, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<AttributeViewModel> GetAttributeAsync(int attributeId)
        {
            AttributeViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Attribute>();
            var attribute = await repository.GetByIDAsync(attributeId);
            if (attribute != null)
            {
                item = Mapper.Map<AttributeViewModel>(attribute);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<AttributeViewModel> SaveAttributeAsync(AttributeViewModel attribute)
        {
            Verify.ArgumentNotNull(attribute, nameof(attribute));
            Attribute attributeModel;
            var repository = UnitOfWork.GetAsyncRepository<Attribute>();
            if (attribute.Id == 0)
            {
                attributeModel = Mapper.Map<Attribute>(attribute);
                await InsertAsync(repository, attributeModel);
            }
            else
            {
                attributeModel = await repository.GetByIDAsync(attribute.Id);
                if (attributeModel != null)
                {
                    await UpdateAsync(repository, attributeModel, attribute);
                }
            }

            return Mapper.Map<AttributeViewModel>(attributeModel);
        }

        /// <inheritdoc/>
        public async Task DeleteAttributeAsync(int attributeId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Attribute>();
            var attribute = await repository.GetByIDAsync(attributeId);
            if (attribute != null)
            {
                await DeleteAsync(repository, attribute);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAttributesAsync(IList<int> attributeIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Attribute>();
            foreach (int attributeId in attributeIds)
            {
                var attribute = await repository.GetByIDAsync(attributeId);
                if (attribute != null)
                {
                    await DeleteNoLogAsync(repository, attribute);
                }
            }

            await OnEntityGroupDeleted(attributeIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Attribute; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(AttributeViewModel attributeViewModel, Attribute attribute)
        {
            attribute.Name = attributeViewModel.Name;
            attribute.EnName = attributeViewModel.EnName;
            attribute.Description = attributeViewModel.Description;
            attribute.Type = attributeViewModel.Type;
            attribute.IsActive = attributeViewModel.IsActive;
        }

        /// <inheritdoc/>
        protected override string GetState(Attribute entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9}",
                    AppStrings.Name, entity.Name, AppStrings.EnName, entity.EnName,
                    AppStrings.Description, entity.Description, AppStrings.Type, entity.Type,
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
