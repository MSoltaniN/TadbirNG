import { Directive, Input } from "@angular/core";
import { Validator, AbstractControl, NG_VALIDATORS } from "@angular/forms";



@Directive({
    selector: '[sppsConfirmEqualValidator]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: ConfirmEqualValidator,
        multi: true
    }]
})

export class ConfirmEqualValidator implements Validator {
    @Input() sppsConfirmEqualValidator: string;
    validate(control: AbstractControl): { [key: string]: any; } | null {
        const controlToCompare = control.parent.get(this.sppsConfirmEqualValidator);
        if (controlToCompare && controlToCompare.value !== control.value) {
            return { 'notEqual': true };
        }
        return null;
    }

}