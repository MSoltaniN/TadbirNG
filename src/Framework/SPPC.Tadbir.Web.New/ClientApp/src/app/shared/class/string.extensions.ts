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
