import { Currency } from "@sppc/finance/models";

export interface PayReceive {
    id: number,
    fiscalPeriodId: number,
    branchId: number,
    /**
    * شماره فرم دریافت/پرداخت
    */
    payReceiveNo: string,

    /**
    * شماره رفرنس
    */
    reference: string;
    /**
    * شناسه کاربر صادر کننده
    */
    issuedById: number,

    /**
    * شناسه آخرین کاربر تغییر دهنده اطلاعات
    */
    modifiedById: number,

    /**
    * شناسه کاربر تأییدکننده ?
    */
    confirmedById: number,

    /**
    * شناسه کاربر تصویب‌ کننده ?
    */
    approvedById: number,

    /**
    * نوع فرم؛ 0 برای دریافت و 1 برای پرداخت
    */
    type: number,

    /**
    * نرخ ارز ?
    */
    currencyRate: number,

    /**
    * شرح
    */
    description: string,

    /**
    * تاریخ ایجاد فرم
    */
    createdDate: Date,

    /**
    * نام کامل کاربر صادرکننده
    */
    issuedByName: string,

    /**
    * نام کامل کاربر تغییردهنده
    */
    modifiedByName: string,

    /**
    * نام کامل کاربر تأییدکننده
    */
    confirmedByName: string,

    /**
    * نام کامل کاربر تصویب‌کننده
    */
    approvedByName: string,

    /**
    * پول یا ارز مورد استفاده در فرم دریافت/پرداخت
    */
    currency: number
}