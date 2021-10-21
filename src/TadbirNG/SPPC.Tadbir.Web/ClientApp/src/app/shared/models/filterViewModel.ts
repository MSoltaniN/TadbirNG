
export class FilterViewModel {
  id: number;
  name: string;
  viewId: number;
  userId: number;
  isPublic: boolean;
  values: string; //json parse tp Array<FilterRow>
}
