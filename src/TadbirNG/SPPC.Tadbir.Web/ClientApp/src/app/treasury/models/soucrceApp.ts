import { IEntity } from "@sppc/shared/models";

export interface SourceApp extends IEntity {
    id:number;

    /**
    * کد منبع یا مصرف
    */
    code:string;

    /**
    * نام منبع یا مصرف
    */
    name:string;

    /**
    * شرح منبع یا مصرف
    */
    description:string;

    /**
    * نوع (منابع: صفر ، مصارف: یک) 
    */
    type:number;

    fiscalPeriodId: number,
    branchId: number,
    branchScope: number,
}

export enum soucrceAppType {
    Source = 0,
    App = 1
}