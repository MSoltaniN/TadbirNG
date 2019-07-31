import { ColumnViewDeviceConfig } from "./columnViewDeviceConfig";
import { IEntity } from "./IEntity";


export interface ColumnViewConfig extends IEntity{

    name: string;
    large: ColumnViewDeviceConfig;
    medium: ColumnViewDeviceConfig;
    small: ColumnViewDeviceConfig;
    extraSmall: ColumnViewDeviceConfig;    
    extraLarge: ColumnViewDeviceConfig;    
}
