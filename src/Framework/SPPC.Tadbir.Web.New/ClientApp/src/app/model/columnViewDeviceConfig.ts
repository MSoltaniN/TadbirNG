import { IEntity } from "./IEntity";

export interface ColumnViewDeviceConfig extends IEntity{

    designIndex : number;
    width?: number;
    index?: number
    visibility: string;
    title: string;
    
}
