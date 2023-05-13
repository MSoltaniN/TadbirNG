import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2 } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DetailComponent } from "@sppc/shared/class";
import { UserValue } from "@sppc/shared/models/userValue";
import { BrowserStorageService, ErrorHandlingService, MetaDataService } from "@sppc/shared/services";
import { UserValueService } from "@sppc/shared/services/userValue.service";
import { ToastrService } from "ngx-toastr";
import { lastValueFrom } from "rxjs";
import { SearchItem } from "../selectForm/selectForm.component";


@Component({
    selector: 'user-value',
    templateUrl: './user-value.component.html',
    styles: [`
      ::ng-deep.sppc-select-form > .k-grid {
        min-height: 600px;
      }
      ::ng-deep.sppc-select-form .k-grid-norecords td {
        vertical-align: top;
      }
      .sppc-select-form {
        max-width: 600px;
      }
      .sppc-select-form {
        min-height: 450px;
      }
      .select-mode {
        display: flex;
        gap: 10px;
        padding: 8px;
      }
      .select-mode input{
        vertical-align: text-top;
      }
      .select-mode input,.select-mode label{
        cursor: pointer;
      }
    `]
})
export class UserValueComponent extends DetailComponent implements OnInit {

    constructor(public toastrService: ToastrService,
        public translate: TranslateService,
        public bStorageService: BrowserStorageService,
        public renderer: Renderer2,
        public metadata: MetaDataService,
        public elem:ElementRef,
        private userValueService: UserValueService,
        private errorHandlingService: ErrorHandlingService)
     {
       super(toastrService, translate, bStorageService, renderer, metadata, '', null,elem);
     }

    @Input() categoryId: number;
    
    categoriesList: Array<SearchItem> = [];
    mode:'select'|'new' = 'select';
    selectedItem: UserValue;
    customName: string;
    customType: string = '1';

    @Output() result: EventEmitter<any> = new EventEmitter();
    @Output() cancel: EventEmitter<any> = new EventEmitter();

    get isFormValid() : boolean {
        if (this.mode == 'select' && this.selectedItem) {
            return true;
        } else if (this.mode == 'new' && this.customName) {
            return true;
        } else {
            return false;
        }        
    }

    ngOnInit(): void {}

    onCancel(): void {
        this.cancel.emit();
    }

    escPress() {
        this.cancel.emit();
    }

    onSelectRow() {
        if (this.mode == 'select' ) {
            this.result.emit({ dataItem: this.selectedItem, viewId: this.viewId });
        } else {
            let item:UserValue = {
                id:0,
                categoryId: this.categoryId,
                value: this.customName
            }
            this.userValueService.insertItem(item).subscribe({
                next: (res) => {
                    this.result.emit({ dataItem: item, viewId: this.viewId });
                },
                error: (err) => {
                    if (err)
                        this.errorMessages =
                        this.errorHandlingService.handleError(err);
                }
            })
        }
    }

    onSelecteKey(item:UserValue) {
        this.selectedItem = item;
    }

    getCategoriesList(data) {
        this.categoriesList = data;
    }

    onSave(e) {}
}