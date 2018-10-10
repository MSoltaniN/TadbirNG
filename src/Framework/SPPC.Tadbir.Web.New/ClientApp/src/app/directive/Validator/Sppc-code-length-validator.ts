import { Directive, ElementRef, Input } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import { DefaultComponent } from '../../class/default.component';

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
  @Input() EntityLevel: number;
  validate(c: AbstractControl): { [key: string]: any; } {

    if (this.SppcCodeLength && this.EntityLevel) {
      var config = this.defultComponent.getViewTreeSettings(this.SppcCodeLength);

      if (config) {
        var viewConfig = config.levels.find(f => f != null && f.no == this.EntityLevel);

        var inputValue = c.value;

        if (inputValue && viewConfig && inputValue.length != viewConfig.codeLength) {
          return { 'sppcCodeLength': true };
        }
      }
    }
  }

}
