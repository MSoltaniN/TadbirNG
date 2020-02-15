using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مورد نیاز برای محاسبه گردش و مانده را برای مراکز هزینه پیاده سازی می کند
    /// </summary>
    public class CostCenterUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public CostCenterUtility(IRepositoryContext context, IConfigRepository config)
            : base(context, config)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مرکز هزینه داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <returns>اطلاعات خوانده شده برای مرکز هزینه</returns>
        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<CostCenter>(itemId);
        }

        /// <summary>
        /// عبارت شرطی مورد نیاز برای انجام محاسبات مرکز هزینه را برمی گرداند
        /// </summary>
        /// <param name="costCenter">اطلاعات مرکز هزینه مورد نظر</param>
        /// <returns>عبارت شرطی</returns>
        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity costCenter)
        {
            return line => line.CostCenter.FullCode.StartsWith(costCenter.FullCode);
        }
    }
}
