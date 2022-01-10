
export class Error {
  messages: Array<string>;
  type: ErrorType;
  statusCode: number;
}

export enum ErrorType {
  NoError = 0,
  ValidationError = 1,
  RuntimeException = 2,
  ExpiredSession = 3,
  NotActivated = 4,  
  BadLicense = 5,
  RequiresOnlineLicense = 6,   
  TooManySessions = 7
}
