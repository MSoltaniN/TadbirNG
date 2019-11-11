import { Braces } from "../models";

//*** this class add for filter values for gridview */

export class Filter{    

  constructor(public FieldName: string, public Value: string, public Operator: string, public fieldTypeName: string,
    public braces: Array<Braces> = null, public id: string = null)
    {     
    }
}
