using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.CrossCutting.Aspects
{
    /// <summary>
    /// امکان ایجاد لاگ های سیستمی را پیش و پس از انجام عملیات یک کلاس فراهم می کند
    /// </summary>
    /// <typeparam name="T">نوع کلاسی که برای عملیات عمومی آن لاگ های سیستمی ایجاد می شود</typeparam>
    public class LoggingAspect<T> : DispatchProxy
    {
        /// <summary>
        /// یک نمونه از کلاس تزیین شده با لاگ سیستمی را ایجاد می کند
        /// </summary>
        /// <param name="decorated">نمونه ای که باید با امکان ایجاد لاگ سیستمی تزیین شود</param>
        /// <param name="logInfo">تابعی که برای ایجاد لاگ سیستمی از نوع اطلاعاتی باید استفاده شود</param>
        /// <param name="logError">تابعی که برای ایجاد لاگ سیستمی از نوع خطا باید استفاده شود</param>
        /// <param name="serializeFunction">
        /// تابعی که امکان ایجاد یک رشته متنی از اطلاعاتی نمونه تزیین شده را فراهم می کند
        /// </param>
        /// <param name="loggingScheduler">سرویس مورد استفاده برای زمان بندی وظایف در حالت اجرای آسنکرون</param>
        /// <returns>نمونه تزیین شده</returns>
        public static T Create(T decorated, Action<string> logInfo, Action<string> logError,
            Func<object, string> serializeFunction, TaskScheduler loggingScheduler = null)
        {
            object proxy = Create<T, LoggingAspect<T>>();
            ((LoggingAspect<T>)proxy).SetParameters(decorated, logInfo, logError, serializeFunction, loggingScheduler);

            return (T)proxy;
        }

        /// <summary>
        /// امکان فراخوانی غیرمستقیم عملیات عمومی در نمونه تزیین شده را فراهم می کند
        /// </summary>
        /// <param name="targetMethod">تابعی که یکی از عملیات عمومی نمونه تزیین شده را پیاده سازی می کند</param>
        /// <param name="args">آرگومان های مورد نیاز تابع فراخانی شده</param>
        /// <returns>مقدار خروجی تابع فراخوانی شده</returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            if (targetMethod != null)
            {
                try
                {
                    try
                    {
                        LogBefore(targetMethod, args);
                    }
                    catch (Exception ex)
                    {
                        // Do not stop method execution if exception
                        LogException(ex);
                    }

                    var result = targetMethod.Invoke(_decorated, args);
                    if (result is Task resultTask)
                    {
                        resultTask.ContinueWith(task =>
                        {
                            if (task.Exception != null)
                            {
                                LogException(task.Exception.InnerException ?? task.Exception, targetMethod);
                            }
                            else
                            {
                                object taskResult = null;
                                if (task.GetType().GetTypeInfo().IsGenericType &&
                                    task.GetType().GetGenericTypeDefinition() == typeof(Task<>))
                                {
                                    var property = task.GetType().GetTypeInfo().GetProperties()
                                        .FirstOrDefault(p => p.Name == "Result");
                                    if (property != null)
                                    {
                                        taskResult = property.GetValue(task);
                                    }
                                }

                                LogAfter(targetMethod, args, taskResult);
                            }
                        },
                            _loggingScheduler);
                    }
                    else
                    {
                        try
                        {
                            LogAfter(targetMethod, args, result);
                        }
                        catch (Exception ex)
                        {
                            // Do not stop method execution if exception
                            LogException(ex);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    if (ex is TargetInvocationException)
                    {
                        LogException(ex.InnerException ?? ex, targetMethod);
                        throw ex.InnerException ?? ex;
                    }
                }
            }

            throw new ArgumentException(nameof(targetMethod));
        }

        private void SetParameters(T decorated, Action<string> logInfo, Action<string> logError,
            Func<object, string> serializeFunction, TaskScheduler loggingScheduler)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }

            _decorated = decorated;
            _logInfo = logInfo;
            _logError = logError;
            _serializeFunction = serializeFunction;
            _loggingScheduler = loggingScheduler ?? TaskScheduler.FromCurrentSynchronizationContext();
        }

        private string GetStringValue(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            if (obj.GetType().GetTypeInfo().IsPrimitive || obj.GetType().GetTypeInfo().IsEnum || obj is string)
            {
                return obj.ToString();
            }

            try
            {
                return _serializeFunction?.Invoke(obj) ?? obj.ToString();
            }
            catch
            {
                return obj.ToString();
            }
        }

        private void LogException(Exception exception, MethodInfo methodInfo = null)
        {
            try
            {
                var errorMessage = new StringBuilder();
                errorMessage.AppendLine($"Class {_decorated.GetType().FullName}");
                errorMessage.AppendLine($"Method {methodInfo?.Name} threw exception");
                errorMessage.AppendLine(exception.Message);

                _logError?.Invoke(errorMessage.ToString());
            }
            catch (Exception)
            {
                // ignored
                // Method should return original exception
            }
        }

        private void LogAfter(MethodInfo methodInfo, object[] args, object result)
        {
            var afterMessage = new StringBuilder();
            afterMessage.AppendLine($"Class {_decorated.GetType().FullName}");
            afterMessage.AppendLine($"Method {methodInfo.Name} executed");
            afterMessage.AppendLine("Output:");
            afterMessage.AppendLine(GetStringValue(result));

            var parameters = methodInfo.GetParameters();
            if (parameters.Any())
            {
                afterMessage.AppendLine("Parameters:");
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var arg = args[i];
                    afterMessage.AppendLine($"{parameter.Name}:{GetStringValue(arg)}");
                }
            }

            _logInfo?.Invoke(afterMessage.ToString());
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            var beforeMessage = new StringBuilder();
            beforeMessage.AppendLine($"Class {_decorated.GetType().FullName}");
            beforeMessage.AppendLine($"Method {methodInfo.Name} executing");
            var parameters = methodInfo.GetParameters();
            if (parameters.Any())
            {
                beforeMessage.AppendLine("Parameters:");

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var arg = args[i];
                    beforeMessage.AppendLine($"{parameter.Name}:{GetStringValue(arg)}");
                }
            }

            _logInfo?.Invoke(beforeMessage.ToString());
        }

        private T _decorated;
        private Action<string> _logInfo;
        private Action<string> _logError;
        private Func<object, string> _serializeFunction;
        private TaskScheduler _loggingScheduler;
    }
}
