import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  private isBusy = false;

  get busy() {
    return this.isBusy;
  }

  show() {
    this.isBusy = true;
  }

  hide() {
    this.isBusy = false;
  }
}
