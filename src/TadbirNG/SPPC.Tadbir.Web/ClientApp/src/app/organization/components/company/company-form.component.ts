import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { CompanyDb } from '@sppc/organization/models';
import { DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { FormControl, Validators } from '@angular/forms';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'company-form-component',
  styles: [`
        input[type=text],textarea ,input[type=password]{ width: 100%; }
    `],
  templateUrl: './company-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class CompanyFormComponent extends DetailComponent implements OnInit {

  @Input() public model: CompanyDb;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public isWizard: boolean = false;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CompanyDb> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Company, ViewName.Company,elem);
  }

  ngOnInit(): void {
    this.editForm.reset();

    setTimeout(() => {
      this.editForm.reset(this.model);
      this.editForm.get('dbName').setValidators([Validators.required, Validators.maxLength(128), Validators.pattern("^[a-zA-Z-_]+$")]);
    })
  }

  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.save.emit(this.editForm.value);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }

  dbNameValue: string;
  toEnglishChars(e:KeyboardEvent) {
    let char = e.code.toString().split('Key');

    if (char.length > 1 && !(e.code == 'KeyA' && e.ctrlKey == true)) {

      if (char[1].toLowerCase() == e.key.toLowerCase()) {

        this.editForm.patchValue({
          dbName: this.dbNameValue+e.key
        });

      } else {

        if (e.shiftKey) {
          this.editForm.patchValue({
            dbName: this.dbNameValue+char[1]
          });
        } else {

          this.editForm.patchValue({
            dbName: this.dbNameValue+char[1].toLowerCase()
          });
        }
      }
    }

    // to Allow the shortcut select all in input text
    if (e.code == 'KeyA' && e.ctrlKey == true) {
      (<any>e.target).select();
      // return;
    } else {
      this.dbNameValue = this.editForm.value.dbName? this.editForm.value.dbName: '';
    }

  }
   
}
