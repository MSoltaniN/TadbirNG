﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// یکی از انواع مختلف اسناد مالی یا اداری که برای طبقه بندی مستندات استفاده می شود
    /// </summary>
    public partial class DocumentType : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public DocumentType()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام طبقه بندی سند مالی یا اداری
        /// </summary>
        public virtual string Name { get; set; }
    }
}
