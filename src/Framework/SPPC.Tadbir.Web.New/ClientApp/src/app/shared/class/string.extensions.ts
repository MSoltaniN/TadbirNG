String.prototype.replaceBadChars = function (value: string): string {

  value = value.replace('ي', 'ی');
  value = value.replace('ك', 'ک');

  return value;
};
