import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sinMask'
})
export class SinMaskPipe implements PipeTransform {

  transform(sin: string | null): string {
    if (!sin) {
      return '-';
    }

    // Ensure SIN is at least 9 characters long for masking
    const sinStr = sin.trim();
    if (sinStr.length < 9) {
      return sinStr;  // Return as is if it's too short
    }

    // Mask all but the last 3 digits
    const maskedPart = '******';
    const visiblePart = sinStr.slice(-3);

    return `${maskedPart}${visiblePart}`;
  }
}
