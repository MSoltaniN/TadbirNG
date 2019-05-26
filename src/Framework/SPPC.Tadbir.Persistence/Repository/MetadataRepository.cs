﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از دیتابیس را پیاده سازی می کند
    /// </summary>
    public class MetadataRepository : IMetadataRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public MetadataRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.UseSystemContext();
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
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetSingleByCriteriaAsync(vu => vu.Name == viewName, vu => vu.Columns);
            var metadata = _mapper.Map<ViewViewModel>(viewMetadata);
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
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetSingleByCriteriaAsync(vu => vu.Id == viewId, vu => vu.Columns);
            var metadata = _mapper.Map<ViewViewModel>(viewMetadata);
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
            var repository = _unitOfWork.GetAsyncRepository<Command>();
            var topCommands = await repository
                .GetEntityQuery()
                .Include(cmd => cmd.Children)
                    .ThenInclude(cmd => cmd.Children)
                        .ThenInclude(cmd => cmd.Children)
                .Where(cmd => cmd.Parent == null && cmd.TitleKey != "Profile")
                .ToListAsync();
            return topCommands
                .Select(cmd => _mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دستورات پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دستورات در منوی پیش فرض کاربران</returns>
        public async Task<IList<CommandViewModel>> GetDefaultCommandsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Command>();
            var profileCommands = await repository.GetSingleByCriteriaAsync(
                cmd => cmd.TitleKey == "Profile", cmd => cmd.Children);
            return profileCommands.Children
                .Select(cmd => _mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        #region System Designer

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای مجوزهای امنیتی موجود را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات فراداده ای مجوزهای امنیتی موجود</returns>
        public async Task<IList<PermissionGroupViewModel>> GetPermissionGroupsAsync()
        {
            var viewModels = new List<PermissionGroupViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<PermissionGroup>();
            var permissions = await repository.GetAllAsync(grp => grp.Permissions);
            Array.ForEach(permissions.ToArray(), perm =>
            {
                var viewModel = _mapper.Map<PermissionGroupViewModel>(perm);
                viewModel.Permissions.AddRange(
                    perm.Permissions.Select(item => _mapper.Map<PermissionViewModel>(item)));
                viewModels.Add(viewModel);
            });
            return viewModels;
        }

        #endregion

        private string GetDynamicColumnSettings(ColumnViewModel column)
        {
            var columnConfig = new ColumnViewConfig(column.Name);
            var deviceConfig = new ColumnViewDeviceConfig()
            {
                Title = column.Name,
                Visibility = column.Visibility ?? ColumnVisibility.Visible,
                Width = 100,
                Index = column.DisplayIndex,
                DesignIndex = column.DisplayIndex
            };
            columnConfig.ExtraLarge = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.ExtraSmall = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Large = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Medium = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Small = (ColumnViewDeviceConfig)deviceConfig.Clone();
            return JsonHelper.From(columnConfig, false);
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
