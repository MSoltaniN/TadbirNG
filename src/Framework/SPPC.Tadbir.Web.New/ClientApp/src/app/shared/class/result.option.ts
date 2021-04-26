import { Error } from "@sppc/shared/models";

/** این کلاس تنظیمات بازگشت از تابه ویرایش و درج را نگهداری میکند */
export class ResultOption {

  constructor(public success: boolean, public error?: Error) {

  }
}
