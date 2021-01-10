import { FormLabelConfig } from "@sppc/config/models/formLabelConfig";
import { IEntity } from "@sppc/shared/models";

export interface FormLabelFullConfig extends IEntity {
  current: FormLabelConfig;
  default: FormLabelConfig;
}
