
export interface QuickReportColumnConfig {
  name: string;
  title: string;
  width: number;
  displayIndex: number;
  userTitle: string;  
  type: string;
  visible: boolean;
}

export class QuickReportColumnConfigInfo implements QuickReportColumnConfig {
  name: string;
  title: string;
  width: number;
  displayIndex: number;
  userTitle: string;
  type: string;
  visible: boolean;
  dataType: string;
}
