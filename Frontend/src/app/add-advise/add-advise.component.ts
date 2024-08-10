import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdviseService } from '../service/advise.service';
import { LoaderService } from '../service/loader.service';
import { NotifyService } from '../service/notify.service';
import { SettingMessage } from '../core/model/toster.model';

@Component({
  selector: 'app-add-advise',
  templateUrl: './add-advise.component.html',
  styleUrl: './add-advise.component.scss',
})
export class AddAdviseComponent implements OnInit {
  public userForm: FormGroup;
  public AdvisorId!: number;
  public characterCount: number = 0;
  private originalSIN: string = '';
  private isDisabled: boolean = false;

  constructor(
    private fb: FormBuilder,
    private loaderService: LoaderService,
    private notifyService: NotifyService,
    private readonly route: ActivatedRoute,
    private adviseService: AdviseService,
    private readonly router: Router
  ) {
    this.userForm = new FormGroup({
      AdvisorId: new FormControl(0),
      Name: new FormControl('', [
        Validators.required,
        Validators.maxLength(255),
      ]),
      SIN: new FormControl('', [
        Validators.required,
        Validators.maxLength(9),
        this.sinValidator(),
      ]),
      Address: new FormControl('', [Validators.maxLength(255)]),
      Phone: new FormControl('', [
        Validators.maxLength(8),
        Validators.minLength(8),
      ]),
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam !== null) {
        this.AdvisorId = +idParam;
        this.fetchadvise(this.AdvisorId);
      }
    });
  }

  public sinValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const valid = /^\d{9}$/.test(control.value);
      return valid ? null : { invalidSIN: { value: control.value } };
    };
  }

  public updateCharacterCount(): void {
    const commentControl = this.userForm.get('Comment');
    if (commentControl?.value) {
      this.characterCount = commentControl.value.length;
    } else {
      this.characterCount = 0;
    }
    // Update isDisabled based on characterCount
    this.isDisabled = this.characterCount > 256;
  }

  public fetchadvise(AdvisorId: number) {
    this.loaderService.show();
    this.adviseService.getAdviceById(AdvisorId).subscribe(
      (res: any) => {
        const response = res.value;
        if (Array.isArray(response)) {
          if (response.length > 0) {
            const advise = response[0];
            this.userForm.patchValue({
              AdvisorId: advise.AdvisorId,
              Name: advise.Name,
              SIN: advise.Sin,
              Address: advise.Address,
              Phone: advise.Phone,
            });
            this.loaderService.hide();
          } else {
            console.error('Advisor not found');
            this.loaderService.hide();
          }
        } else {
          const advise = res;
          this.userForm.patchValue({
            AdvisorId: advise.AdvisorId,
            name: advise.Name,
            Sin: advise.Sin,
            Address: advise.Address,
            Phone: advise.Phone,
          });
          this.loaderService.hide();
        }
      },
      (error: any) => {
        console.error('Error fetching advisor', error);
        this.loaderService.hide();
      }
    );
  }

  public onMaskChange({
    masked,
    original,
  }: {
    masked: string;
    original: string;
  }) {
    // This method will be called when the value changes in the MaskDirective
    this.originalSIN = original; // Store the original SIN value
    this.userForm.get('SIN')?.setValue(masked, { emitEvent: false });
  }

  public alphabateNotallowed(data: any): void {
    const input = data.key;
    const currentValue = (data.target as HTMLInputElement).value;

    // Allow only numeric characters
    if (!/^\d$/.test(input)) {
      data.preventDefault();
    } else if (currentValue.length >= 9) {
      // Prevent additional digits if length is 9
      data.preventDefault();
    }
    if (data.keyCode >= 65 && data.keyCode <= 96) {
      data.prevantDefault();
    } else if (data.keyCode >= 97 && data.keyCode <= 122) {
      data.prevantDefault();
    } else if (data.keyCode == 42 && 43 && 45 && 47) {
      data.prevantDefault();
    }
  }

  public onPaste(event: ClipboardEvent): void {
    // Retrieve the pasted data from the clipboard
    const clipboardData = event.clipboardData?.getData('text') || '';

    // Check if the pasted data length exceeds 9 characters
    if (clipboardData.length > 9) {
      event.preventDefault(); // Prevent the paste operation
    }
  }

  public alphabateNotallowedphone(data: any): void {
    const input = data.key;
    const currentValue = (data.target as HTMLInputElement).value;

    // Allow only numeric characters
    if (!/^\d$/.test(input)) {
      data.preventDefault();
    } else if (currentValue.length >= 8) {
      // Prevent additional digits if length is 9
      data.preventDefault();
    }
    if (data.keyCode >= 65 && data.keyCode <= 96) {
      data.prevantDefault();
    } else if (data.keyCode >= 97 && data.keyCode <= 122) {
      data.prevantDefault();
    } else if (data.keyCode == 42 && 43 && 45 && 47) {
      data.prevantDefault();
    }
  }

  public onPastephone(event: ClipboardEvent): void {
    // Retrieve the pasted data from the clipboard
    const clipboardData = event.clipboardData?.getData('text') || '';

    // Check if the pasted data length exceeds 9 characters
    if (clipboardData.length > 8) {
      event.preventDefault();
    }
  }

  public onSubmit() {
    this.loaderService.show();
    const formValue = this.userForm.value;
    const phoneValue = formValue.Phone.replace(/\D/g, '');
    const dataToSubmit = {
      name: formValue.Name,
      address: formValue.Address,
      sin: formValue.SIN,
      phone: String(phoneValue),
    };
    if (formValue.AdvisorId) {
      const editdata = {
        advisorId: formValue.AdvisorId,
        name: formValue.Name,
        address: formValue.Address,
        sin: formValue.SIN,
        phone: String(phoneValue),
      };
      this.adviseService.update(formValue.AdvisorId, editdata).subscribe({
        next: (res: any) => {
          this.loaderService.hide();
          this.notifyService.showSuccess(SettingMessage.updateSuccess);
          this.router.navigate(['']);
        },
        error: (err: any) => {
          this.loaderService.hide();
          console.error('Error updating Advisor:', err);
          this.notifyService.showWarning(SettingMessage.updateError);
        },
      });
    } else {
      this.adviseService.create(dataToSubmit).subscribe({
        next: (res: any) => {
          this.loaderService.hide();
          this.notifyService.showSuccess(SettingMessage.addSuccess);
          this.router.navigate(['']);
        },
        error: (err: any) => {
          this.loaderService.hide();
          console.error('Error creating advisor:', err);
        },
      });
    }
  }

  public goBack() {
    this.router.navigate(['']);
  }
}
