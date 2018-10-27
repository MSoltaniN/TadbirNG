using System;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// کلاس پایه که امکانات اولیه عملیات دیتابیسی را در اختیار کلاس های مشتق شده قرار می دهد
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        protected RepositoryBase(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Metadata = metadata;
        }

        /// <summary>
        /// پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی
        /// </summary>
        protected IAppUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی
        /// </summary>
        protected IDomainMapper Mapper { get; }

        /// <summary>
        /// امکان خواندن متادیتا برای یک موجودیت را فراهم می کند
        /// </summary>
        protected IMetadataRepository Metadata { get; }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// <para>توجه : فراخوانی این متد با اطلاعات محیطی معتبر برای موفقیت سایر عملیات این کلاس الزامی است</para>
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public virtual void SetCurrentContext(UserContextViewModel userContext)
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            _currentContext = userContext;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه
        /// </summary>
        protected UserContextViewModel _currentContext;
    }
}
