import { Directive, ElementRef, Input } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import { DefaultComponent } from '@sppc/shared/class';

@Directive({
  selector: '[SppcCodeLength]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: SppcCodeLengthDirective,
    multi: true
  }]
})
export class SppcCodeLengthDirective implements Validator {

  constructor(private el: ElementRef, private defultComponent: DefaultComponent) { }

  @Input() SppcCodeLength: number;
  @Input() ViewId: number;
  validate(c: AbstractControl): { [key: string]: any; } {

    if (this.SppcCodeLength && this.ViewId) {
      var config = this.defultComponent.getViewTreeSettings(this.ViewId);

      if (config) {
        var viewConfig = config.levels.find(f => f != null && f.no == this.SppcCodeLength);

        var inputValue = c.value;

        if (inputValue && viewConfig && inputValue.length != viewConfig.codeLength) {
          return { 'sppcCodeLength': true };
        }
      }
    }
  }

}
