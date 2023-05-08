import { IEntity } from "@sppc/shared/models";

export interface SourceApp extends IEntity {
    id:number;

    /**
    * کد منبع یا مصرف
    */
    Code:string;

    /**
    * نام منبع یا مصرف
    */
    Name:string;

    /**
    * شرح منبع یا مصرف
    */
    Description:string;

    /**
    * نوع (منابع: صفر ، مصارف: یک) 
    */
    Type:number;

    fiscalPeriodId: number,
    branchId: number,
    branchScope: number,
}