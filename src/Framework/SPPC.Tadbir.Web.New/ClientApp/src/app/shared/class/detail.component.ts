import { BaseComponent } from "./base.component";
import { Injectable, Renderer2, Optional, Inject, Host, Input, HostListener } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormControl, ValidatorFn, Validators } from "@angular/forms";
import { Property } from "./metadata/property";
import { String } from './source';
import { MetaDataService, BrowserStorageService, SessionKeys } from "../services";
import { ShortcutCommand } from "../models/shortcutCommand";
import { ShortcutService } from "../services/shortcut.service";
import { ServiceLocator } from "@sppc/service.locator";



@Injectable()
export class DetailComponent extends BaseComponent  {

  shortcuts: ShortcutCommand[] = [new ShortcutCommand(1, "Ctrl+Shift+Y", "addNew")];

  private form: FormGroup;
  public properties: Map<string, Array<Property>>;

  public metadataKey: string;
  public isEnableSaveBtn: boolean = true;

  @Input() public errorMessages: string[];

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, private metadataService: MetaDataService,
    @Optional() @Inject('empty') public entityType: string, @Optional() @Inject('empty') public viewId: number,public shortcutService?:ShortcutService) {
    super(toastrService, bStorageService);

    if (viewId > 0) {
      this.metadataKey = String.Format(SessionKeys.MetadataKey, this.viewId ? this.viewId.toString() : '', this.CurrentLanguage);

      this.localizeMsg();
      var propertiesValue = this.bStorageService.getMetadata(this.metadataKey);

      this.properties = new Map<string, Array<Property>>();
      if (propertiesValue && propertiesValue != null) {
        var result = JSON.parse(propertiesValue);
        this.properties.set(this.metadataKey, result.columns);
      }
    }

    this.errorMessages = [];
    this.shortcutService = ServiceLocator.injector.get(ShortcutService); 
  }

  public get editForm(): FormGroup {
    if (this.form == undefined) {

      this.form = new FormGroup({ id: new FormControl() });
      if (!this.properties.get(this.metadataKey)) {
        this.metadataService.getMetaDataById(this.viewId).finally(() => {
          this.fillFormValidators();
          return this.form;
        }).subscribe((res1: any) => {
          this.properties.set(this.metadataKey, res1.columns);          
          this.bStorageService.setMetadata(this.metadataKey, res1);
          return
        });
      }
    }
    else {
      this.fillFormValidators();
    }

    return this.form;

  }

  private fillFormValidators() {
    var p: Property | undefined = undefined;
    if (this.properties.get(this.metadataKey) == undefined) return;

    for (let entry of this.properties.get(this.metadataKey)) {

      var name: string = entry.name.toLowerCase().substring(0, 1) + entry.name.substring(1);

      var validators: ValidatorFn[] = [];

      if (entry.length > 0) validators.push(Validators.maxLength(entry.length));

      if (entry.minLength > 0) validators.push(Validators.minLength(entry.minLength));

      if (!entry.isNullable) validators.push(Validators.required);

      if (!this.form.contains(name) && name.toLowerCase() != "rowno") {
        this.form.addControl(name, new FormControl("", validators));
      }
    }

  }

  private initializeFrom() {

    if (this.properties[this.metadataKey] == undefined) {

      this.metadataService.getMetaDataById(this.viewId).finally(() => {
        this.fillFormValidators();

      }).subscribe((res1: any) => {

        this.properties[this.metadataKey] = res1.properties;
        this.bStorageService.setMetadata(this.metadataKey, this.properties[this.metadataKey]);

        return
      });

    }
    else {
      this.fillFormValidators();
    }

  }

  /**
   * this method localize CRUD messages 
   */
  private localizeMsg() {
    // read message format for crud operations      
    var entityType = '';
    this.translate.get("Entity." + this.entityType).subscribe((msg: string) => {
      entityType = msg;
    });

    this.translate.get("Messages.Inserted").subscribe((msg: string) => {
      this.insertMsg = String.Format(msg, entityType);
    });

    this.translate.get("Messages.Updated").subscribe((msg: string) => {
      this.updateMsg = String.Format(msg, entityType);;
    });

    this.translate.get("Messages.Deleted").subscribe((msg: string) => {
      this.deleteMsg = String.Format(msg, entityType);;
    });

    this.translate.get("Messages.DeleteConfirm").subscribe((msg: string) => {
      this.deleteConfirmMsg = String.Format(msg, entityType);
    });


  }


  public getText(key: string): string {
    var msgText = '';
    this.translate.get(key).subscribe((msg: string) => {
      msgText = msg;
    });
    return msgText;
  }

  /**
   * برای هندل کردن شورکات های که به یک متد خاص متصل میباشند
   * @param event
   */
  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key != "Control" && event.key != "Shift" && event.key != "Alt") {
      console.log(event);

      var ctrl = event.ctrlKey ? true : false;
      var shift = event.shiftKey ? true : false;
      var alt = event.altKey ? true : false;

      var key = event.code.replace('Key', '').toLowerCase();

      var shortcutCommand = this.shortcutService.searchShortcutCommand(ctrl, shift, alt, key, this.shortcuts);
      if (shortcutCommand) {
        if (this[shortcutCommand.method] != undefined)        
          this[shortcutCommand.method]();
      }

    }
  }

  

}
