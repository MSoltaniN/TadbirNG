using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات مورد نیاز برای یک موجودیت پایه با ساختار سلسله مراتبی (درختی، چندسطحی) را تعریف می کند
    /// </summary>
    public interface ITreeEntity : IBaseEntity
    {
        /// <summary>
        /// کد شناسایی برای سطح جاری موجودیت چندسطحی در ساختار درختی
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل موجودیت چندسطحی متشکل از کدهای تمام سطوح قبلی در ساختار درختی
        /// </summary>
        string FullCode { get; set; }

        /// <summary>
        /// نام موجودیت چندسطحی
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// شماره سطح که عمق این موجودیت چندسطحی را در ساختار درختی مشخص می کند
        /// </summary>
        short Level { get; set; }
    }
}
