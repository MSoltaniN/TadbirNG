import { ViewTreeLevelConfig } from ".";



export interface ViewTreeConfig {
  viewId: number;
  maxDepth: number;
  levels: Array<ViewTreeLevelConfig>;
}
