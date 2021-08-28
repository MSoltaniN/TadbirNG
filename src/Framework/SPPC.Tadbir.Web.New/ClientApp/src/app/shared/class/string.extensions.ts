String.prototype.replaceBadChars = function (value: string): string {

  var chars = value.split('');
  var newValue = '';
  chars.forEach((char) => {
    var newChar = char;
    newChar = newChar.replace('ي', 'ی');
    newChar = newChar.replace('ك', 'ک');

    newValue += newChar;
  });
  
  return newValue;
};


String.prototype.toPersianNumbers = function (value: string): string {

  var chars = value.split('');
  var newValue = '';
  chars.forEach((char) => {
    var newChar = char;
    newChar = newChar.replace('0', '۰');
    newChar = newChar.replace('1', '۱');
    newChar = newChar.replace('2', '۲');
    newChar = newChar.replace('3', '۳');
    newChar = newChar.replace('4', '۴');
    newChar = newChar.replace('5', '۵');
    newChar = newChar.replace('6', '۶');
    newChar = newChar.replace('7', '۷');
    newChar = newChar.replace('8', '۸');
    newChar = newChar.replace('9', '۹');

    newValue += newChar;
  });

  return newValue;
};
