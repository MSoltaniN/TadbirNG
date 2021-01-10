import { IEntity } from "@sppc/shared/models";

interface IDictionary<TValue> {
  [id: string]: TValue;
}

export interface FormLabelConfig extends IEntity {
  formId: number;
  localeId: number;
  labelMap: IDictionary<string>;
}

export class FormLabelConfigEntity implements FormLabelConfig {    
  constructor(public formId: number = 0, public localeId: number = 0, public labelMap: IDictionary<string> = undefined)
  {

  }  
}
