export { };
declare global {
  interface String {
   
    replaceBadChars(value: string): string;

    toPersianNumbers(value: string): string;
  }
}
