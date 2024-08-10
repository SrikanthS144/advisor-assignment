import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Output,
} from '@angular/core';

@Directive({
  selector: '[appMask]',
})
export class MaskDirective {
  @Output() valueChanged: EventEmitter<{ masked: string; original: string }> =
    new EventEmitter<{ masked: string; original: string }>();
  constructor(public ref: ElementRef) {}

  @HostListener('input')
  public masking() {
    const inputElement = this.ref.nativeElement;
    let originalValue = inputElement.value;
    let maskedValue = '';

    if (originalValue.length === 0) {
      maskedValue = ' '; // or '' if you prefer
    } else if (originalValue.length < 9) {
      maskedValue =
        originalValue.substring(0, originalValue.length - 1) +
        '*' +
        originalValue.substring(originalValue.length);
    } else {
      maskedValue = originalValue;
    }

    inputElement.value = maskedValue;
    this.valueChanged.emit({ masked: maskedValue, original: originalValue });
  }
}
