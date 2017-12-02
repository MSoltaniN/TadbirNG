import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { Account } from '../../model/index';

@Component({
    selector: 'account-form-component',
    styles: [
        "input[type=text] { width: 100%; }"
    ],
    templateUrl: './account2-form.component.html'
})

export class AccountFormComponent {
    private editForm = new FormGroup({
        'accountId': new FormControl("", Validators.required),
        'code': new FormControl("", Validators.required),
        'name': new FormControl("", Validators.required),   
        'description': new FormControl(),
        'fiscalPeriodId': new FormControl("", Validators.required),
        'branchId': new FormControl("", Validators.required)
    });

    private active: boolean = false;
    @Input() public isNew: boolean = false;

    @Input() public set model(account: Account) {
        this.editForm.reset(account);

        this.active = account !== undefined;
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Account> = new EventEmitter();

    public onSave(e : any): void {
        e.preventDefault();
        this.save.emit(this.editForm.value);
        this.active = false;
    }

    public onCancel(e : any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.active = false;
        this.cancel.emit();
    }
}