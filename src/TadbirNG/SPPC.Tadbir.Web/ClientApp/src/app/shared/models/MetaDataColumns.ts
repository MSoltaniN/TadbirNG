import { IEntity } from "./IEntity";


export interface MetaDataColumns extends IEntity {
    allowFiltering: boolean;
    allowSorting: boolean;
    displayIndex: number;
    dotNetType: string;
    expression: any;
    groupName: string;
    id: number;
    isDynamic: boolean;
    isFixedLength: boolean;
    isNullable: boolean;
    length: number;
    minLength: number;
    name: string;
    scriptType: string;
    settings: string;
    storageType: string;
    type: number | string;
    viewId: 69;
    visibility: string;
}
