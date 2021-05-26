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
    var ctrlFound: boolean = false;
    var altFound: boolean = false;
    var shiftFound: boolean = false;
    var keyFound: boolean = false;

    var it = command.hotKey.toLowerCase();
    if (ctrl && it.indexOf('ctrl') >= 0) {
      ctrlFound = true;
    }

    if (it.indexOf('alt') >= 0) {
      if (alt) {
        altFound = true;
      }
    }

    if (it.indexOf('shift') >= 0) {
      if (shift) {
        shiftFound = true;
      }
    }

    if (it.indexOf('+' + key) >= 0)
      keyFound = true;


    if (ctrl && shift && alt) {
      if ((ctrlFound && shiftFound && altFound) && keyFound)
        return command;
    }
    else if (ctrl && shift) {
      if ((ctrlFound && shiftFound) && keyFound)
        return command;
    }
    else if (ctrl && alt) {
      if ((ctrlFound && altFound) && keyFound)
        return command;
    }
    else if (shift && alt) {
      if ((shiftFound && altFound) && keyFound)
        return command;
    }
    else if (ctrl) {
      if ((ctrlFound) && keyFound)
        return command;
    }
  }
}

