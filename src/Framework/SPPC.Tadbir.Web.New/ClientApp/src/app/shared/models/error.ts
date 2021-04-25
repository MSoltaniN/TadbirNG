
export class Error {
  messages: Array<string>;
  type: ErrorType;
}

export enum ErrorType {
  Info = 0,
  Warning = 1,
  ValidationError = 2,
  RuntimeException = 3
}
