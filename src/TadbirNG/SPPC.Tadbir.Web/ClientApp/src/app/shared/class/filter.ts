import { Braces } from "../models";
import './string.extensions';
//*** this class add for filter values for gridview */

export class Filter{    

  constructor(public FieldName: string, public Value: string, public Operator: string, public fieldTypeName: string,
    public braces: Array<Braces> = null, public id: string = null)
    {    
      this.Value = this.Value.toString().replaceBadChars(this.Value);    
    }
}
