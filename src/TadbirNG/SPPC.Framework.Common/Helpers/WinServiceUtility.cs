using System;
using System.Linq;
using System.Text;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// عملیات کمکی برای کار با سرویس های ویندوزی را پیاده سازی می کند
    /// </summary>
    public class WinServiceUtility
    {
        /// <summary>
        /// یک سرویس ویندوزی جدید را با مشخصات داده شده به سرویس های موجود اضافه می کند.
        /// </summary>
        /// <param name="name">نام سیستمی سرویس جدید</param>
        /// <param name="displayName">نام نمایشی سرویس جدید به صورتی که در فهرست سرویس ها دیده می شود</param>
        /// <param name="binPath">مسیر فایل اجرایی اصلی سرویس</param>
        /// <param name="typeMode">مدل اجرایی سرویس که به طور پیش فرض در پروسس جداگانه اجرا می شود</param>
        /// <param name="startMode">نوع راه اندازی سرویس که به طور پیش فرض هنگام شروع به کار سیستم عامل اجرا می شود</param>
        /// <param name="errorMode">شکل گزارش خطای سرویس که به طور پیش فرض به صورت عادی انجام می شود</param>
        /// <returns>در صورتی موفق بودن عملیات مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public static bool Install(
            string name, string displayName, string binPath, string typeMode = "own",
            string startMode = "auto", string errorMode = "normal")
        {
            bool installed = true;
            var runner = new CliRunner();
            var output = runner.Run($"sc query {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains($"SERVICE_NAME: {name}"))
            {
                output = runner.Run(
                    GetCreateCommand(name, displayName, binPath, typeMode, startMode, errorMode));
                lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                installed = !lines[0].Contains("FAILED");
            }

            return installed;
        }

        /// <summary>
        /// سرویس مشخص شده با نام سیستمی را از فهرست سرویس ها حذف می کند.
        /// </summary>
        /// <param name="name">نام سیستمی سرویس مورد نظر برای حذف</param>
        /// <returns>در صورتی موفق بودن عملیات مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public static bool Uninstall(string name)
        {
            var runner = new CliRunner();
            var output = runner.Run($"sc delete {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return !lines[0].Contains("FAILED");
        }

        /// <summary>
        /// سرویس مشخص شده با نام سیستمی را راه اندازی حذف می کند.
        /// </summary>
        /// <param name="name">نام سیستمی سرویس مورد نظر برای راه اندازی</param>
        /// <returns>در صورتی موفق بودن عملیات مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public static bool Start(string name)
        {
            bool started = false;
            var runner = new CliRunner();
            var output = runner.Run($"sc start {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run($"sc query {name}");
                    started = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("RUNNING"))
                        .Any();
                } while (!started);
            }

            return started;
        }

        /// <summary>
        /// سرویس مشخص شده با نام سیستمی را از متوقف می کند.
        /// </summary>
        /// <param name="name">نام سیستمی سرویس مورد نظر برای توقف کار</param>
        /// <returns>در صورتی موفق بودن عملیات مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public static bool Stop(string name)
        {
            bool stopped = false;
            var runner = new CliRunner();
            var output = runner.Run($"sc stop {name}");
            var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (!lines[0].Contains("FAILED"))
            {
                do
                {
                    output = runner.Run($"sc query {name}");
                    stopped = output
                        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => line.Contains("STATE") && line.Contains("STOPPED"))
                        .Any();
                } while (!stopped);
            }

            return stopped;
        }

        private static string GetCreateCommand(
            string name, string displayName, string binPath, string typeMode, string startMode, string errorMode)
        {
            var cmdBuilder = new StringBuilder($"sc create {name}");
            cmdBuilder.Append($" type= {typeMode} start= {startMode} error= {errorMode}");
            cmdBuilder.Append($" displayname= \"{displayName}\"");
            cmdBuilder.Append($" binpath= \"{binPath}\"");
            return cmdBuilder.ToString();
        }
    }
}
