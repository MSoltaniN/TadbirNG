import { ViewTreeLevelConfig } from "../model/index";


export interface ViewTreeConfig {
  viewId: number;
  maxDepth: number;
  levels: Array<ViewTreeLevelConfig>;
}
