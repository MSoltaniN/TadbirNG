import { ShortcutCommand } from "../models/shortcutCommand";

export class ShortcutService {
    

  /**
   * برای جستجو در شورت کات های عملیاتی بکار میرود
   * @param ctrl
   * @param shift
   * @param alt
   * @param key
   * @param commands
   */
  searchShortcutCommand(ctrl: boolean, shift: boolean, alt: boolean, key: string, commands: ShortcutCommand[]): ShortcutCommand {
    for (let command of commands) {
      if (command.hotKey != null) {

        var result = this.hotkeyUsed(ctrl, alt, shift, key, command);
        if (result) return result;
      }
    }
    return undefined;
  }

  /**
   * در یک کامند جستجو میکند و تطابق شورت کات زا در کامند بررسی میکند
   * @param ctrl
   * @param alt
   * @param shift
   * @param key
   * @param command
   */
  hotkeyUsed(ctrl: boolean, alt: boolean, shift: boolean, key: string, command: any) {
    // ['ctrl','alt','shift','key']
    var keys : string[] = ['0','0','0','0'];
    var matchKeys : string[] = [];

    matchKeys[0] = ctrl ? '1' : '0';
    matchKeys[1] = alt ? '1' : '0';
    matchKeys[2] = shift ? '1' : '0';
    matchKeys[3] = '1';

    var it = command.hotKey.toLowerCase();
    if (it.indexOf('ctrl') >= 0) {
      keys[0] = '1';
    }

    if (it.indexOf('alt') >= 0) {
      keys[1] = '1';
    }
       
    if (it.indexOf('shift') >= 0) {
      keys[2] = '1';
    }
    
    var onlykey = it.replace('alt','').replace('ctrl','').replace('shift','');
    if (onlykey.indexOf(key) >= 0)
    {
      keys[3] = '1';
    }      
       
    var keyStr = keys.join('');
    var matchKeyStr = matchKeys.join('');

    if(parseInt(keyStr,2) == parseInt(matchKeyStr,2))
    {
      return command;
    }
  }
}

