import { IEntity } from "./IEntity";

export interface TreeNode extends IEntity{
    id: number;
    parentId?: number;
    name: string;
}


export class TreeNodeInfo implements TreeNode {
    constructor(public id: number = 0,
        public parentId: number | undefined,
        public name: string) { }
}