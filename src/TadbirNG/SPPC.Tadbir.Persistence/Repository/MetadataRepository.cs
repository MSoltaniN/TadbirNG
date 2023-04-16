using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using SPPC.Tadbir.Resources;
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
        public MetadataRepository(IRepositoryContext context, IConfigRepository config)
            : base(context)
        {
            _config = config;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataAsync(string viewName)
        {
            return await GetViewMetadataByCriteriaAsync(vu => vu.Name == viewName);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataByIdAsync(int viewId)
        {
            return await GetViewMetadataByCriteriaAsync(vu => vu.Id == viewId);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تمام کلیدهای میانبر را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از کلیدهای میانبر</returns>
        public async Task<IList<ShortcutCommandViewModel>> GetShortcutCommandsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<ShortcutCommand>();
            var shortcuts = await repository
                .GetEntityQuery()
                .Select(sc => Mapper.Map<ShortcutCommandViewModel>(sc))
                .ToListAsync();
            return shortcuts;
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
                    .Where(cmd => cmd.Parent == null && cmd.TitleKey != AppStrings.Profile)
                    .OrderBy(cmd => cmd.Index)
                    .ToListAsync();
            }
            else
            {
                topCommands = await repository
                    .GetEntityQuery()
                    .Include(cmd => cmd.Children)
                    .Where(cmd => cmd.Parent == null && cmd.TitleKey == AppStrings.Organization)
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
            _currentCalendar = await _config.GetCurrentCalendarTypeAsync();
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var views = await repository.GetAllAsync(vu => vu.Columns);

            var viewMetadatas = new List<ViewViewModel>();
            foreach (var view in views)
            {
                viewMetadatas.Add(await LoadViewMetadataAsync(view));
            }

            return viewMetadatas;
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
            var columns = new List<ColumnViewModel>();
            var dynamicColumns = metadata.Columns
                .Where(col => col.IsDynamic)
                .OrderBy(col => col.DisplayIndex)
                .ToList();
            columns.AddRange(metadata.Columns.Except(dynamicColumns));
            Localize(columns);

            if (items.Count() > AppConstants.MaxCompareItems)
            {
                items = items.Take(AppConstants.MaxCompareItems);
            }

            int index = 1;
            foreach (int item in items)
            {
                columns.AddRange(await GetDynamicColumnsAsync(dynamicColumns, itemViewId, item, index));
                foreach (var column in dynamicColumns)
                {
                    column.DisplayIndex += (short)dynamicColumns.Count;
                }

                index++;
            }

            dynamicMetadata.SetColumns(columns);
            return dynamicMetadata;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای پویا را برای گزارش مانده به تفکیک حساب ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای تنظیم شده برای گزارش</param>
        /// <returns>اطلاعات فراداده ای پویا برای کزارش</returns>
        public async Task<ViewViewModel> GetBalanceByAccountMetadataAsync(
            BalanceByAccountParameters parameters)
        {
            var metadata = await GetViewMetadataByIdAsync(ViewId.BalanceByAccount);
            var dynamicMetadata = metadata.GetCopy();
            var columns = new List<ColumnViewModel>
            {
                metadata.Columns
                    .Where(col => col.Name == "RowNo")
                    .Single()
            };
            columns.AddRange(metadata.Columns
                .Where(GetAccountItemCriteria(parameters.ViewId)));
            if (parameters.IsSelectedAccount && parameters.ViewId != ViewId.Account)
            {
                columns.AddRange(metadata.Columns
                    .Where(GetAccountItemCriteria(ViewId.Account)));
            }

            if (parameters.IsSelectedDetailAccount && parameters.ViewId != ViewId.DetailAccount)
            {
                columns.AddRange(metadata.Columns
                    .Where(GetAccountItemCriteria(ViewId.DetailAccount)));
            }

            if (parameters.IsSelectedCostCenter && parameters.ViewId != ViewId.CostCenter)
            {
                columns.AddRange(metadata.Columns
                    .Where(GetAccountItemCriteria(ViewId.CostCenter)));
            }

            if (parameters.IsSelectedProject && parameters.ViewId != ViewId.Project)
            {
                columns.AddRange(metadata.Columns
                    .Where(GetAccountItemCriteria(ViewId.Project)));
            }

            columns.AddRange(metadata.Columns
                .Where(col => col.DisplayIndex >= 9));
            int index = 1;
            Array.ForEach(columns.Skip(1).ToArray(), col => col.DisplayIndex = (short)index++);
            Localize(columns);

            dynamicMetadata.SetColumns(columns);
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

        private static string GetDynamicColumnSettings(ColumnViewModel column)
        {
            var columnConfig = GetDynamicColumnConfig(column);
            return JsonHelper.From(columnConfig, false);
        }

        private static ColumnViewConfig GetDynamicColumnConfig(ColumnViewModel column, int width = 100)
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

        private static Func<ColumnViewModel, bool> GetAccountItemCriteria(int viewId)
        {
            Func<ColumnViewModel, bool> criteria;
            switch (viewId)
            {
                case ViewId.Account:
                    criteria = column => column.Name.StartsWith("Account")
                        && column.Name != "AccountDescription";
                    break;
                case ViewId.DetailAccount:
                    criteria = column => column.Name.StartsWith("DetailAccount");
                    break;
                case ViewId.CostCenter:
                    criteria = column => column.Name.StartsWith("CostCenter");
                    break;
                case ViewId.Project:
                    criteria = column => column.Name.StartsWith("Project");
                    break;
                default:
                    criteria = null;
                    break;
            }

            return criteria;
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

        private async Task<ViewViewModel> GetViewMetadataByCriteriaAsync(Expression<Func<View, bool>> criteria)
        {
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var metadata = await repository
                .GetSingleByCriteriaAsync(criteria, vu => vu.Columns);
            return await LoadViewMetadataAsync(metadata);
        }

        private async Task<ViewViewModel> LoadViewMetadataAsync(View listMetadata)
        {
            var listMetadataView = Mapper.Map<ViewViewModel>(listMetadata);
            foreach (var column in listMetadataView.Columns)
            {
                column.Settings = GetDynamicColumnSettings(column);
            }

            await PrepareColumnsAsync(listMetadataView);
            return listMetadataView;
        }

        private async Task PrepareColumnsAsync(ViewViewModel view)
        {
            var calendar = _currentCalendar ?? await _config.GetCurrentCalendarTypeAsync();
            var columns = view.Columns
                .OrderBy(col => col.DisplayIndex)
                .ToArray();
            Array.ForEach(columns, col =>
            {
                if (col.DotNetType.Contains("System.Date"))
                {
                    var columnCalendar = (CalendarType)Enum.Parse(typeof(CalendarType), col.Type);
                    if (columnCalendar == CalendarType.Default)
                    {
                        col.Type = calendar.ToString();
                    }
                }
            });
            view.SetColumns(columns);
        }

        private readonly IConfigRepository _config;
        private CalendarType? _currentCalendar;
    }
}
