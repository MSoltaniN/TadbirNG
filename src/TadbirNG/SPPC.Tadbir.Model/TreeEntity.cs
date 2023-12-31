﻿using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// اطلاعات مورد نیاز برای یک موجودیت پایه با ساختار سلسله مراتبی (درختی، چندسطحی) را نگهداری می کند
    /// </summary>
    public class TreeEntity : BaseEntity, ITreeEntity
    {
        /// <summary>
        /// شناسه دیتابیسی موجودیت والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// کد شناسایی برای سطح جاری موجودیت چندسطحی در ساختار درختی
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل موجودیت چندسطحی متشکل از کدهای تمام سطوح قبلی در ساختار درختی
        /// </summary>
        public virtual string FullCode { get; set; }

        /// <summary>
        /// نام موجودیت چندسطحی
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// شماره سطح که عمق این موجودیت چندسطحی را در ساختار درختی مشخص می کند
        /// </summary>
        public virtual short Level { get; set; }

        /// <summary>
        /// شرحی که اطلاعات تکمیلی برای موجودیت چندسطحی را مشخص می کند
        /// </summary>
        public virtual string Description { get; set; }
    }
}
