import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'


interface Item {
  key: number,
  value: string
}

@Component({
  selector: 'sppc-branch-scope',
  template: `
               <kendo-dropdownlist class="ddl-branch-scope" [data]="scopeData" [valuePrimitive]="true" [disabled]="!isNew && !isEnableInEditMode"
                                   [textField]="'value'" [(ngModel)]="scopeSelected" [value]="scopeSelected" [valueField]="'key'"
                                   (valueChange)="onPermissionChange($event)">        
                            <ng-template kendoDropDownListValueTemplate let-dataItem>
                                {{ dataItem?.value | translate }}
                            </ng-template>
                            <ng-template kendoDropDownListItemTemplate let-dataItem>
                                {{ dataItem?.value | translate }}
                            </ng-template>
               </kendo-dropdownlist>
`,
  styles: [`::ng-deep .ddl-branch-scope { width:100% }`],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SppcBranchScope),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SppcBranchScope),
      multi: true,
    }
  ]
})
export class SppcBranchScope implements OnInit, ControlValueAccessor, Validator {

  private parseError: boolean = false;
  public scope: Array<Item>;
  public scopeData: Array<Item>;
  permission: number;
  scopeSelected: number;
  _parentScope: number = 0;

  @Input() set parentScope(ps: number) {
    this._parentScope = ps;
  }
  @Input() public isNew: boolean;
  @Input() public isEnableInEditMode: boolean = false;

  propagateChange: any = () => { };


  constructor() {
    this.scope = [
      { value: "BranchScope.AllBranches", key: 0 },
      { value: "BranchScope.CurrentBranchAndSubsets", key: 1 },
      { value: "BranchScope.CurrentBranch", key: 2 }
    ];
  }

  ngOnInit() {
    this.scopeData = this.scope.filter(f => f.key >= this._parentScope);
    if (this.isNew) {
      this.scopeSelected = this._parentScope;

      setTimeout(() => {
        this.propagateChange(this.scopeSelected);
      })
    }
  }

  onPermissionChange(e: any) {
    if (this.scopeSelected && this.scopeSelected >= 0) {
      this.parseError = false;
      this.propagateChange(this.scopeSelected);
    }
    else {
      this.parseError = true;
    }
  }


  writeValue(value: any): void {
    if (value != undefined) {
      if (!this.isNew)
        this.scopeSelected = value;
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {
    //this.propagateChange = fn;
  }

  public validate(control: FormControl) {
    return (!this.parseError) ? null : {
      jsonParseError: {
        valid: false,
      },
    };
  }


}
