import { ColumnViewDeviceConfig } from "./columnViewDeviceConfig";


export interface ColumnViewConfig {

    name: string;
    large: ColumnViewDeviceConfig;
    medium: ColumnViewDeviceConfig;
    small: ColumnViewDeviceConfig;
    extraSmall: ColumnViewDeviceConfig;    
    extraLarge: ColumnViewDeviceConfig;    
}
