import { ReloadStatusType } from "../enum";



/** این کلاس تنظیمات تابع ریلود را نگهداری میکند */
export class ReloadOption{    

  constructor(public InsertedModel?: any, public Parameter?: any, public Status?: ReloadStatusType)
  {
    
  }
}
