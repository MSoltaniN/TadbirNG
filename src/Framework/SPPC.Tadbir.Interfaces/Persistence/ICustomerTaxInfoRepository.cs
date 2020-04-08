using SPPC.Tadbir.ViewModel.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مالیاتی طرف حساب را تعریف میکند
    /// </summary>
    public interface ICustomerTaxInfoRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی طرف حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="customerTax">اطلاعات مالیاتی طرف حساب</param>
        /// <returns>اطلاعات مالیاتی طرف حساب ایجاد یا اصلاح شده</returns>
        Task<CustomerTaxInfoViewModel> SaveCustomerTaxInfoAsync(CustomerTaxInfoViewModel customerTax);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="customerTaxId">شناسه عددی اطلاعات مالیاتی مورد نظر برای حذف</param>
        Task DeleteCustomerTaxInfoAsync(int customerTaxId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مالیاتی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="customerTaxIds">مجموعه ای از شناسه های عددی اطلاعات مالیاتی مورد نظر برای حذف</param>
        Task DeleteCustomerTaxInfosAsync(IList<int> customerTaxIds);
    }
}
