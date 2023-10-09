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
    /// عملیات مورد نیاز برای مدیریت فایل ها را پیاده سازی می کند
    /// </summary>
    public class FileRepository : EntityLoggingRepository<File, FileViewModel>, IFileRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public FileRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<FileViewModel>> GetFilesAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var files = new List<FileViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<File>(ViewId.File);
                files = await query
                    .Select(item => Mapper.Map<FileViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<FileViewModel>(files, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<FileViewModel> GetFileAsync(int fileId)
        {
            FileViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<File>();
            var file = await repository.GetByIDAsync(fileId);
            if (file != null)
            {
                item = Mapper.Map<FileViewModel>(file);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<FileViewModel> SaveFileAsync(FileViewModel file)
        {
            Verify.ArgumentNotNull(file, nameof(file));
            File fileModel;
            var repository = UnitOfWork.GetAsyncRepository<File>();
            if (file.Id == 0)
            {
                fileModel = Mapper.Map<File>(file);
                SetBaseEntityInfo(fileModel);
                await InsertAsync(repository, fileModel);
            }
            else
            {
                fileModel = await repository.GetByIDAsync(file.Id);
                if (fileModel != null)
                {
                    await UpdateAsync(repository, fileModel, file);
                }
            }

            return Mapper.Map<FileViewModel>(fileModel);
        }

        /// <inheritdoc/>
        public async Task DeleteFileAsync(int fileId)
        {
            var repository = UnitOfWork.GetAsyncRepository<File>();
            var file = await repository.GetByIDAsync(fileId);
            if (file != null)
            {
                await DeleteAsync(repository, file);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteFilesAsync(IList<int> fileIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<File>();
            foreach (int fileId in fileIds)
            {
                var file = await repository.GetByIDAsync(fileId);
                if (file != null)
                {
                    await DeleteNoLogAsync(repository, file);
                }
            }

            await OnEntityGroupDeleted(fileIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.File; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(FileViewModel fileViewModel, File file)
        {
            file.Name = fileViewModel.Name;
            file.Description = fileViewModel.Description;
            file.IsActive = fileViewModel.IsActive;
        }

        /// <inheritdoc/>
        protected override string GetState(File entity)
        {
            return entity == null 
                ? String.Empty
                : $"{AppStrings.FileName} : {entity.Name}" +
                 $"{AppStrings.Description} : {entity.Description}";
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
