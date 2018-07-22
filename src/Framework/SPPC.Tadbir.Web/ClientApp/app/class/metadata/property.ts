//** property interface for metadata */

export interface Property {

     dotNetType: string; 
     id: number;
     isFixedLength: boolean;
     isNullable: boolean;
     length: number;
     name: string;
     nameResourceId: string;
     scriptType: string;
     storageType: string;
     minLength: number;
     expression: string;
}