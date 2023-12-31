﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات متداول برای کار با مولفه های بردار حساب با ساختار درختی را پیاده سازی می کند
    /// </summary>
    public class AccountItemRepository : RepositoryBase, IAccountItemRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="repository">
        /// عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند
        /// </param>
        public AccountItemRepository(IRepositoryContext context, ISecureRepository repository)
            : base(context)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafAccountsAsync(
            GridOptions gridOptions = null)
        {
            var accounts = await _repository.GetAllAsync<Account>(ViewId.Account, acc => acc.Children);
            var leafAccounts = accounts
                .Where(acc => acc.Children.Count == 0)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToList();
            return leafAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafDetailAccountsAsync(
            GridOptions gridOptions = null)
        {
            var detailAccounts = await _repository.GetAllAsync<DetailAccount>(
                ViewId.DetailAccount, facc => facc.Children);
            var leafDetails = detailAccounts
                .Where(facc => facc.Children.Count == 0)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .Apply(gridOptions)
                .ToList();
            return leafDetails;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafCostCentersAsync(
            GridOptions gridOptions = null)
        {
            var costCenters = await _repository.GetAllAsync<CostCenter>(
                ViewId.CostCenter, cc => cc.Children);
            var leafCenters = costCenters
                .Where(cc => cc.Children.Count == 0)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .Apply(gridOptions)
                .ToList();
            return leafCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه ها در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafProjectsAsync(
            GridOptions gridOptions = null)
        {
            var projects = await _repository.GetAllAsync<Project>(ViewId.Project, prj => prj.Children);
            var leafProjects = projects
                .Where(prj => prj.Children.Count == 0)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .Apply(gridOptions)
                .ToList();
            return leafProjects;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootAccountsAsync(
            GridOptions gridOptions = null)
        {
            var accounts = await _repository.GetAllAsync<Account>(ViewId.Account, acc => acc.Children);
            var rootAccounts = accounts
                .Where(acc => acc.ParentId == null)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToList();
            return rootAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync(
            GridOptions gridOptions = null)
        {
            var details = await _repository.GetAllAsync<DetailAccount>(
                ViewId.DetailAccount, acc => acc.Children);
            var rootDetails = details
                .Where(facc => facc.ParentId == null)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .Apply(gridOptions)
                .ToList();
            return rootDetails;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync(
            GridOptions gridOptions = null)
        {
            var centers = await _repository.GetAllAsync<CostCenter>(
                ViewId.CostCenter, cc => cc.Children);
            var rootCenters = centers
                .Where(cc => cc.ParentId == null)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .Apply(gridOptions)
                .ToList();
            return rootCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه ها در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync(
            GridOptions gridOptions = null)
        {
            var projects = await _repository.GetAllAsync<Project>(ViewId.Project, prj => prj.Children);
            var rootProjects = projects
                .Where(prj => prj.ParentId == null)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .Apply(gridOptions)
                .ToList();
            return rootProjects;
        }

        private readonly ISecureRepository _repository;
    }
}
