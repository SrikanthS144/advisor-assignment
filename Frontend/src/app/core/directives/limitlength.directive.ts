import { Directive, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appLimitlength]',
})
export class LimitlengthDirective {
  @Input('appLimitLength') maxLength: number = 12;

  constructor(private ngControl: NgControl) {}

  @HostListener('input', ['$event.target.value'])
  public onInput(value: string) {
    if (value.length > this.maxLength) {
      const newValue = value.slice(0, this.maxLength);
      this.ngControl.control?.setValue(newValue, { emitEvent: false });
    }
  }
}
