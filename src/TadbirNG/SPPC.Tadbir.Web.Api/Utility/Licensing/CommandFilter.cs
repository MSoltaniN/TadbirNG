using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.Web.Api;

namespace SPPC.Tadbir.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای فیلتر کردن دستورات منوی برنامه را تعریف می کند
    /// </summary>
    public class CommandFilter : ICommandFilter
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز سرویس وب را فراهم می کند</param>
        public CommandFilter(IApiPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// فهرست داده شده برای دستورات منوی برنامه را با توجه به یک محدودیت ویرایشی فیلتر می کند
        /// </summary>
        /// <param name="commands">فهرستی از دستورات منوی برنامه</param>
        /// <param name="limit">محدودیت ویرایشی برای فیلتر دستورات منو</param>
        public void FilterCommands(IList<CommandViewModel> commands, EditionLimit limit)
        {
            var config = GetEditionConfig();
            if (limit == EditionLimit.RowPermissionAccess && !config.EnableRowPermissions)
            {
                // NOTE: The systematic approach would be searching recursively for required command.
                // Here, a slightly more optimized search is performed based on known command level...
                foreach (var command in commands)
                {
                    var rowPermCommand = command.Children
                        .Where(cmd => cmd.Id == (int)CommandId.RowAccessSettings)
                        .FirstOrDefault();
                    if (rowPermCommand != null)
                    {
                        command.Children.Remove(rowPermCommand);
                        break;
                    }
                }
            }
        }

        private EditionConfig GetEditionConfig()
        {
            using var reader = new StreamReader(
                typeof(Program).Assembly.GetManifestResourceStream(_pathProvider.Edition));
            string jsonConfig = reader.ReadToEnd();
            return JsonHelper.To<EditionConfig>(jsonConfig);
        }

        private readonly IApiPathProvider _pathProvider;
    }
}
