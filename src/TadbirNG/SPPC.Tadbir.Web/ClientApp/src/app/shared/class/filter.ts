import { Braces } from "../models";
import './string.extensions';
import * as moment from "jalali-moment";
//*** this class add for filter values for gridview */

export class Filter{    

  constructor(public FieldName: string, public Value: string, public Operator: string, public fieldTypeName: string,
    public braces: Array<Braces> = null, public id: string = null)
    {    
      this.Value = this.Value.toString().replaceBadChars(this.Value);    
       if(localStorage.getItem('lang')=="fa" && fieldTypeName.toLocaleLowerCase()=="system.date"){
         debugger;
         let jalaliDate = this.Value ;
         let gregorianDate = moment.from(jalaliDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
         this.Value=gregorianDate;
       }
    }
}
