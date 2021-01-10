using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مالیاتی طرف حساب را تعریف میکند
    /// </summary>
    public class CustomerTaxInfoRepository
        : EntityLoggingRepository<CustomerTaxInfo, CustomerTaxInfoViewModel>, ICustomerTaxInfoRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CustomerTaxInfoRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی طرف حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="customerTax">اطلاعات مالیاتی طرف حساب</param>
        /// <returns>اطلاعات مالیاتی طرف حساب ایجاد یا اصلاح شده</returns>
        public async Task<CustomerTaxInfoViewModel> SaveCustomerTaxInfoAsync(CustomerTaxInfoViewModel customerTax)
        {
            Verify.ArgumentNotNull(customerTax, "customerTax");
            CustomerTaxInfo customerTaxInfo = default(CustomerTaxInfo);
            var repository = UnitOfWork.GetAsyncRepository<CustomerTaxInfo>();
            if (customerTax.Id == 0)
            {
                customerTaxInfo = Mapper.Map<CustomerTaxInfo>(customerTax);
                await InsertAsync(repository, customerTaxInfo);
            }
            else
            {
                customerTaxInfo = await repository.GetByIDAsync(customerTax.Id);
                if (customerTaxInfo != null)
                {
                    await UpdateAsync(repository, customerTaxInfo, customerTax);
                }
            }

            return Mapper.Map<CustomerTaxInfoViewModel>(customerTaxInfo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="customerTaxId">شناسه عددی اطلاعات مالیاتی مورد نظر برای حذف</param>
        public async Task DeleteCustomerTaxInfoAsync(int customerTaxId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CustomerTaxInfo>();
            var customerTax = await repository.GetByIDWithTrackingAsync(customerTaxId);
            if (customerTax != null)
            {
                await DeleteAsync(repository, customerTax);
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="customerTaxIds">مجموعه ای از شناسه های عددی اطلاعات مالیاتی مورد نظر برای حذف</param>
        public async Task DeleteCustomerTaxInfosAsync(IList<int> customerTaxIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<CustomerTaxInfo>();
            foreach (int customerTaxId in customerTaxIds)
            {
                var customerTax = await repository.GetByIDWithTrackingAsync(customerTaxId);
                if (customerTax != null)
                {
                    await DeleteNoLogAsync(repository, customerTax);
                }
            }

            await OnEntityGroupDeleted(customerTaxIds);
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="customerTaxInfoViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="customerTaxInfo">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CustomerTaxInfoViewModel customerTaxInfoViewModel, CustomerTaxInfo customerTaxInfo)
        {
            customerTaxInfo.CustomerFirstName = customerTaxInfoViewModel.CustomerFirstName;
            customerTaxInfo.CustomerName = customerTaxInfoViewModel.CustomerName;
            customerTaxInfo.Address = customerTaxInfoViewModel.Address;
            customerTaxInfo.BuyerType = customerTaxInfoViewModel.BuyerType;
            customerTaxInfo.Description = customerTaxInfoViewModel.Description;
            customerTaxInfo.EconomicCode = customerTaxInfoViewModel.EconomicCode;
            customerTaxInfo.MobileNo = customerTaxInfoViewModel.MobileNo;
            customerTaxInfo.NationalCode = customerTaxInfoViewModel.NationalCode;
            customerTaxInfo.PerCityCode = customerTaxInfoViewModel.PerCityCode;
            customerTaxInfo.PersonType = customerTaxInfoViewModel.PersonType;
            customerTaxInfo.PhoneNo = customerTaxInfoViewModel.PhoneNo;
            customerTaxInfo.PostalCode = customerTaxInfoViewModel.PostalCode;
            customerTaxInfo.ProvinceCode = customerTaxInfoViewModel.ProvinceCode;
            customerTaxInfo.CityCode = customerTaxInfoViewModel.CityCode;
        }

        private readonly ISystemRepository _system;
    }
}
