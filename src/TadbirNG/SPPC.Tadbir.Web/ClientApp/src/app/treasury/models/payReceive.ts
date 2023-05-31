import { Currency } from "@sppc/finance/models";

export interface PayReceive {
    id: number,
    fiscalPeriodId: number,
    branchId: number,
    date: Date,
    /**
    * شماره فرم دریافت/پرداخت
    */
    payReceiveNo: string,

    /**
    * شماره رفرنس
    */
    reference: string;

    /**
    * نرخ ارز ?
    */
    currencyRate: number,

    /**
    * شرح
    */
    description: string,

    /**
    * نام کامل کاربر صادرکننده
    */
    issuedByName: string,

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
    currencyId: number,

    isApproved: boolean,
    isConfirmed: boolean
}