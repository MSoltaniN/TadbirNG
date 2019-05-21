import { BaseComponent } from "./base.component";
import { Injectable, Renderer2, Optional, Inject, Host } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from "../service/metadata/metadata.service";
import { FormGroup, FormControl, ValidatorFn, Validators } from "@angular/forms";
import { Property } from "./metadata/property";
import { String } from './source';



@Injectable()
export class DetailComponent extends BaseComponent {

    private form: FormGroup;
    public properties: Map<string, Array<Property>>; 

    constructor(public toastrService: ToastrService, public translate: TranslateService
        , public renderer: Renderer2, private metadataService: MetaDataService,
        @Optional() @Inject('empty') public entityType: string, @Optional() @Inject('empty') public metaDataName: string) {        
        super(toastrService);

      this.localizeMsg();
      var propertiesValue = localStorage.getItem(this.metaDataName)

      this.properties = new Map<string, Array<Property>>(); 
      this.properties.set(this.metaDataName, JSON.parse(propertiesValue));
    }    

  public get editForm(): FormGroup {
        if (this.form == undefined) {

            this.form = new FormGroup({ id: new FormControl() });
            if (!this.properties.get(this.metaDataName)) {              

              this.metadataService.getMetaData(this.metaDataName).finally(() => {

                    this.fillFormValidators();

                    return this.form;

                }).subscribe((res1: any) => {

                    this.properties.set(this.metaDataName, res1.columns);
                  localStorage.setItem(this.metaDataName, JSON.stringify(res1.columns));

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
        if (this.properties.get(this.metaDataName) == undefined) return;

        for (let entry of this.properties.get(this.metaDataName)) {

            var name: string = entry.name.toLowerCase().substring(0, 1) + entry.name.substring(1);

            var validators: ValidatorFn[] = [];

            if (entry.length > 0) validators.push(Validators.maxLength(entry.length));

            if (entry.minLength > 0) validators.push(Validators.minLength(entry.minLength));

            if (!entry.isNullable) validators.push(Validators.required);

            if (!this.form.contains(name)) {
                this.form.addControl(name, new FormControl("", validators));
            }
        }

    }

    private initializeFrom() {

        if (this.properties[this.metaDataName] == undefined) {

            this.metadataService.getMetaData(this.metaDataName).finally(() => {
                this.fillFormValidators();

            }).subscribe((res1: any) => {

                this.properties[this.metaDataName] = res1.properties;

                localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))

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
}
