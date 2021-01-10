using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از دیتابیس را پیاده سازی می کند
    /// </summary>
    public class MetadataRepository : RepositoryBase, IMetadataRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public MetadataRepository(IRepositoryContext context)
            : base(context)
        {
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نوع موجودیت مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فراداده آن مورد نیاز است</typeparam>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataAsync<TEntity>()
            where TEntity : IEntity
        {
            return await GetViewMetadataAsync(typeof(TEntity).Name);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataAsync(string viewName)
        {
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetSingleByCriteriaAsync(vu => vu.Name == viewName, vu => vu.Columns);
            var metadata = Mapper.Map<ViewViewModel>(viewMetadata);
            foreach (var column in metadata.Columns)
            {
                column.Settings = GetDynamicColumnSettings(column);
            }

            metadata.Columns = metadata.Columns
                .OrderBy(col => col.DisplayIndex)
                .ToList();
            return metadata;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataByIdAsync(int viewId)
        {
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetSingleByCriteriaAsync(vu => vu.Id == viewId, vu => vu.Columns);
            var metadata = Mapper.Map<ViewViewModel>(viewMetadata);
            foreach (var column in metadata.Columns)
            {
                column.Settings = GetDynamicColumnSettings(column);
            }

            metadata.Columns = metadata.Columns
                .OrderBy(col => col.DisplayIndex)
                .ToList();
            return metadata;
        }

        /// <summary>
        /// اطلاعات نمایشی تمام دستوراتی که در بالاترین سطح ساختار درختی قرار دارند را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از دستورات در بالاترین سطح</returns>
        public async Task<IList<CommandViewModel>> GetTopLevelCommandsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Command>();
            var topCommands = new List<Command>();
            if (UserContext.FiscalPeriodId > 0 && UserContext.BranchId > 0)
            {
                topCommands = await repository
                .GetEntityQuery()
                .Include(cmd => cmd.Children)
                    .ThenInclude(cmd => cmd.Children)
                        .ThenInclude(cmd => cmd.Children)
                .Where(cmd => cmd.Parent == null && cmd.TitleKey != "Profile")
                .ToListAsync();
            }
            else
            {
                topCommands = await repository
                .GetEntityQuery()
                .Include(cmd => cmd.Children)
                .Where(cmd => cmd.Parent == null && cmd.TitleKey == "Organization")
                .ToListAsync();
            }

            return topCommands
                .Select(cmd => Mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دستورات پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دستورات در منوی پیش فرض کاربران</returns>
        public async Task<IList<CommandViewModel>> GetDefaultCommandsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Command>();
            var profileCommands = await repository.GetSingleByCriteriaAsync(
                cmd => cmd.TitleKey == "Profile", cmd => cmd.Children);
            return profileCommands.Children
                .Select(cmd => Mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای همه موجودیت ها را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns> اطلاعات فراداده ای تعریف شده برای همه موجودیت ها</returns>
        public async Task<IList<ViewViewModel>> GetViewsMetadataAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<View>();
            return await repository
                .GetEntityQuery()
                .Select(item => Mapper.Map<ViewViewModel>(item))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای مرکب برای نمای لیستی مقایسه ای با اقلام داده شده را ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای لیستی مورد نظر</param>
        /// <param name="itemViewId">شناسه دیتابیسی که نوع موجودیت اقلام داده شده را تعیین می کند</param>
        /// <param name="items">مجموعه اقلام داده شده برای مقایسه</param>
        /// <returns>اطلاعات فراداده ای مرکب برای نمای لیستی مقایسه ای</returns>
        public async Task<ViewViewModel> GetCompoundViewMetadataAsync(
            int viewId, int itemViewId, IEnumerable<int> items)
        {
            var metadata = await GetViewMetadataByIdAsync(viewId);
            var dynamicMetadata = metadata.GetCopy();
            dynamicMetadata.Columns = new List<ColumnViewModel>();
            var dynamicColumns = metadata.Columns
                .Where(col => col.IsDynamic)
                .OrderBy(col => col.DisplayIndex)
                .ToList();
            dynamicMetadata.Columns.AddRange(metadata.Columns.Except(dynamicColumns));
            Localize(dynamicMetadata.Columns);

            if (items.Count() > AppConstants.MaxCompareItems)
            {
                items = items.Take(AppConstants.MaxCompareItems);
            }

            int index = 1;
            foreach (int item in items)
            {
                dynamicMetadata.Columns.AddRange(
                    await GetDynamicColumnsAsync(dynamicColumns, itemViewId, item, index));
                foreach (var column in dynamicColumns)
                {
                    column.DisplayIndex += (short)dynamicColumns.Count;
                }

                index++;
            }

            return dynamicMetadata;
        }

        #region System Designer

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای مجوزهای امنیتی موجود را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات فراداده ای مجوزهای امنیتی موجود</returns>
        public async Task<IList<PermissionGroupViewModel>> GetPermissionGroupsAsync()
        {
            var viewModels = new List<PermissionGroupViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<PermissionGroup>();
            var permissions = await repository.GetAllAsync(grp => grp.Permissions);
            Array.ForEach(permissions.ToArray(), perm =>
            {
                var viewModel = Mapper.Map<PermissionGroupViewModel>(perm);
                viewModel.Permissions.AddRange(
                    perm.Permissions.Select(item => Mapper.Map<PermissionViewModel>(item)));
                viewModels.Add(viewModel);
            });
            return viewModels;
        }

        #endregion

        private string GetDynamicColumnSettings(ColumnViewModel column)
        {
            var columnConfig = GetDynamicColumnConfig(column);
            return JsonHelper.From(columnConfig, false);
        }

        private ColumnViewConfig GetDynamicColumnConfig(ColumnViewModel column, int width = 100)
        {
            var columnConfig = new ColumnViewConfig(column.Name);
            var deviceConfig = new ColumnViewDeviceConfig()
            {
                Title = column.Name,
                Visibility = column.Visibility ?? ColumnVisibility.Visible,
                Width = width,
                Index = column.DisplayIndex,
                DesignIndex = column.DisplayIndex
            };
            columnConfig.ExtraLarge = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.ExtraSmall = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Large = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Medium = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Small = (ColumnViewDeviceConfig)deviceConfig.Clone();
            return columnConfig;
        }

        private void Localize(IList<ColumnViewModel> columns)
        {
            foreach (var column in columns)
            {
                var config = JsonHelper.To<ColumnViewConfig>(column.Settings);
                config.ExtraSmall.Title =
                    config.Small.Title =
                    config.Medium.Title =
                    config.Large.Title =
                    config.ExtraLarge.Title = Context.Localize(config.Small.Title);
                column.Settings = JsonHelper.From(config);
            }
        }

        private async Task<IEnumerable<ColumnViewModel>> GetDynamicColumnsAsync(
            IEnumerable<ColumnViewModel> columns, int itemViewId, int item, int index)
        {
            IEnumerable<ColumnViewModel> dynamicColumns = new List<ColumnViewModel>();
            switch (itemViewId)
            {
                case ViewId.CostCenter:
                    dynamicColumns = await GetDynamicEntityColumnsAsync<CostCenter>(columns, item, index);
                    break;
                case ViewId.Project:
                    dynamicColumns = await GetDynamicEntityColumnsAsync<Project>(columns, item, index);
                    break;
                case ViewId.Branch:
                    dynamicColumns = await GetDynamicEntityColumnsAsync<Branch>(columns, item, index);
                    break;
                case ViewId.FiscalPeriod:
                    dynamicColumns = await GetDynamicEntityColumnsAsync<FiscalPeriod>(columns, item, index);
                    break;
                default:
                    break;
            }

            return dynamicColumns;
        }

        private async Task<IEnumerable<ColumnViewModel>> GetDynamicEntityColumnsAsync<TEntity>(
            IEnumerable<ColumnViewModel> columns, int item, int index)
            where TEntity : class, IEntity
        {
            var dynamicColumns = new List<ColumnViewModel>();
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var entity = await repository.GetByIDAsync(item);
            if (entity != null)
            {
                int newIndex = columns.First().DisplayIndex;
                string name = (string)Reflector.GetSimpleProperty(entity, "Name");
                foreach (var column in columns)
                {
                    var dynamicColumn = column.GetCopy();
                    string fieldName = String.Format("{0}{1}", dynamicColumn.Name, index);
                    dynamicColumn.Name = column.Name.Replace("Item", String.Empty);
                    var title = String.Format("{0} - {1}", Context.Localize(dynamicColumn.Name), name);
                    var config = GetDynamicColumnConfig(dynamicColumn, 200);
                    config.Name = fieldName;
                    config.ExtraSmall.Title =
                        config.Small.Title =
                        config.Medium.Title =
                        config.Large.Title =
                        config.ExtraLarge.Title = title;
                    dynamicColumn.Name = fieldName;
                    dynamicColumn.DisplayIndex = (short)newIndex++;
                    dynamicColumn.Settings = JsonHelper.From(config, false);
                    dynamicColumns.Add(dynamicColumn);
                }
            }

            UnitOfWork.UseSystemContext();
            return dynamicColumns;
        }
    }
}
