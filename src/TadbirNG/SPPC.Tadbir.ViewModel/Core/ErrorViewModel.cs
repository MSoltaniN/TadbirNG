using System;
using System.Collections.Generic;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// اطلاعات کلی خطای ایجاد شده سمت سرویس را نگهداری می کند
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ErrorViewModel()
        {
            Messages = new List<string>();
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس را با توجه به نوع خطای داده شده می سازد
        /// </summary>
        public ErrorViewModel(ErrorType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس را با توجه به مقادیر داده شده می سازد
        /// </summary>
        public ErrorViewModel(string message, ErrorType type = ErrorType.ValidationError)
            : this(type)
        {
            Messages.Add(message);
        }

        /// <summary>
        /// مجموعه پیغام های خطای محلی شده با توجه به زبان جاری برنامه
        /// </summary>
        public List<string> Messages { get; private set; }

        /// <summary>
        /// نوع پیغام یا خطای ایجاد شده سمت سرویس
        /// </summary>
        public ErrorType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                SetStatusCode(_type);
            }
        }

        /// <summary>
        /// کد وضعیت مربوط به خطای ایجاد شده - با شماره استاندارد
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// نمایش متنی از این آبجکت ساخته و برمی گرداند
        /// </summary>
        /// <returns>نمایش متنی این آبجکت</returns>
        public override string ToString()
        {
            string mainMessage = Messages.Count > 0
                ? Messages[0]
                : String.Empty;
            return mainMessage;
        }

        private void SetStatusCode(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.ValidationError:
                    StatusCode = 400;
                    break;
                case ErrorType.RuntimeException:
                    StatusCode = 500;
                    break;
                case ErrorType.ExpiredSession:
                    StatusCode = 401;
                    break;
                case ErrorType.NotActivated:
                case ErrorType.BadLicense:
                case ErrorType.RequiresOnlineLicense:
                case ErrorType.TooManySessions:
                    StatusCode = 403;
                    break;
                default:
                    break;
            }
        }

        private ErrorType _type;
    }
}
