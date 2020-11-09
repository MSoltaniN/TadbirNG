using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مورد نیاز برای محاسبه گردش و مانده را برای تفصیلی های شناور پیاده سازی می کند
    /// </summary>
    public class DetailAccountUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        /// <param name="repository">امکان اعمال فیلترهای شعبه و سطری را فراهم می کند</param>
        public DetailAccountUtility(IRepositoryContext context, IConfigRepository config, ISecureRepository repository)
            : base(context, config, repository)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات تفصیلی شناور داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <returns>اطلاعات خوانده شده برای تفصیلی شناور</returns>
        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<DetailAccount>(itemId);
        }

        /// <summary>
        /// عبارت شرطی مورد نیاز برای انجام محاسبات تفصیلی شناور را برمی گرداند
        /// </summary>
        /// <param name="detailAccount">اطلاعات تفصیلی شناور مورد نظر</param>
        /// <returns>عبارت شرطی</returns>
        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity detailAccount)
        {
            return line => line.DetailAccount.FullCode.StartsWith(detailAccount.FullCode);
        }
    }
}
