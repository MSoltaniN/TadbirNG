
export interface EntityType {
  key: string;
  value: string;
  isEntity: boolean;
}

export class EntityTypeInfo implements EntityType {
  key: string;
  value: string;
  isEntity: boolean;
}
