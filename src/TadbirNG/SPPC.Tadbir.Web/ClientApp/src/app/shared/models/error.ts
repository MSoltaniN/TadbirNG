
export class Error {
  messages: Array<string>;
  type: ErrorType;
  statusCode: number;
}

export enum ErrorType {
  NoError = 0,
  ValidationError = 1,
  RuntimeException = 2
}
