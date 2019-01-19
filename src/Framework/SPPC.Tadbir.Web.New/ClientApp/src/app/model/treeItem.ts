export interface TreeItem
{
    
    id : number;

    baseId :number;

    ParentId?: number;

    Name :string;

    ServiceUrl :string;

    IsGroup : boolean;

    IsSystem : boolean;

    IsDefault : boolean;
}

