using System.Collections.Generic;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای فیلتر کردن دستورات منوی برنامه را تعریف می کند
    /// </summary>
    public interface ICommandFilter
    {
        /// <summary>
        /// فهرست داده شده برای دستورات منوی برنامه را با توجه به یک محدودیت ویرایشی فیلتر می کند
        /// </summary>
        /// <param name="commands">فهرستی از دستورات منوی برنامه</param>
        /// <param name="limit">محدودیت ویرایشی برای فیلتر دستورات منو</param>
        void FilterCommands(IList<CommandViewModel> commands, EditionLimit limit);
    }
}
