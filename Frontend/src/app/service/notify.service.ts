import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
export class NotifyService {
  constructor(private notification: MessageService) {}

  public showSuccess(message: string) {
    this.notification.add({
      severity: 'success',
      summary: 'Success',
      detail: message,
    });
  }

  public showError(message: string) {
    return this.notification.add({
      severity: 'error',
      summary: 'Error',
      detail: message,
    });
  }

  public showWarning(message: string) {
    return this.notification.add({
      severity: 'warn',
      summary: 'Warn',
      detail: message,
    });
  }

  public showMessage(status: string, message: string) {
    return this.notification.add({
      severity: status,
      summary: status,
      detail: message,
    });
  }
}
