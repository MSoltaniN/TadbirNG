import { Braces } from "../models";
import './string.extensions';
import * as moment from "jalali-moment";
//*** this class add for filter values for gridview */

export class Filter{    

  constructor(public FieldName: string, public Value: string, public Operator: string, public fieldTypeName: string,
    public braces: Array<Braces> = null, public id: string = null)
    {    
      this.Value = this.Value.toString().replaceBadChars(this.Value);
      const jalaliPattern =/^1[1-5]\d{2}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|([1-2][0-9])|(0[1-9]))))$/g;
      var formatedDate=moment(this.Value).format('YYYY/MM/DD');
      var isJalali=jalaliPattern.test(formatedDate);
      if(isJalali && localStorage.getItem('lang')=="fa" && (fieldTypeName.toLocaleLowerCase()=="system.date" || fieldTypeName.toLocaleLowerCase()=="system.datetime") ){
        let jalaliDate = this.Value ;
        let gregorianDate = moment.from(jalaliDate, 'fa', 'YYYY/MM/DD').locale('en').format('YYYY/MM/DD');
        this.Value=gregorianDate;
       }
    }
}
