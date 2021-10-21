import { QuickSearchColumnConfig } from ".";

export interface QuickSearchConfig {
  viewId: number;
  searchMode: string;
  columns: Array<QuickSearchColumnConfig>;
}
