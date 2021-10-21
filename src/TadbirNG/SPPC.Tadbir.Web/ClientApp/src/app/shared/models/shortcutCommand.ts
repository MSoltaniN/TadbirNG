export class ShortcutCommand {
  constructor(public id: number, public permissionId: number, public name: string,
    public scope: string, public hotKey: string, public method: string) {

  }
}
