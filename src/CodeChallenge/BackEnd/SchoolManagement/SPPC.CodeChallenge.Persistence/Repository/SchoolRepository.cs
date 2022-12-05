using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.CodeChallenge.Mapper;
using SPPC.CodeChallenge.ViewModel.Core;

namespace SPPC.CodeChallenge.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مدارس را پیاده سازی می کند
    /// </summary>
    public class SchoolRepository : ISchoolRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="mapper">امکان تبدیل کلاس ها به یکدیگر را فراهم می کند</param>
        public SchoolRepository(IDomainMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه مدارس را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدارس تعریف شده</returns>
        public async Task<IList<SchoolViewModel>> GetSchoolsAsync()
        {
            // Use EF Core to read all schools from database. Map to view model using _mapper field.
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، مدرسه با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="schoolId">شناسه عددی یکی از مدارس موجود</param>
        /// <returns>مدرسه مشخص شده با شناسه عددی</returns>
        public async Task<SchoolViewModel> GetSchoolAsync(int schoolId)
        {
            // Use EF Core to read a single schools by id from database. Map to view model using _mapper field.
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مدرسه را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="school">مدرسه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مدرسه ایجاد یا اصلاح شده</returns>
        public async Task<SchoolViewModel> SaveSchoolAsync(SchoolViewModel school)
        {
            // Use EF Core to create a new school or update an existing school in database...
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، مدرسه مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="schoolId">شناسه عددی مدرسه مورد نظر برای حذف</param>
        public async Task DeleteSchoolAsync(int schoolId)
        {
            // Use EF Core to delete a single school from database...
            throw new NotImplementedException();
        }

        private readonly IDomainMapper _mapper;
    }
}
