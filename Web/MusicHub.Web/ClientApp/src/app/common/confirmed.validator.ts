import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';

export function ConfirmedValidator(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];
    if (matchingControl.errors && !matchingControl.errors.confirmedValidator) {
      return;
    }
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ confirmedValidator: true });
    } else {
      matchingControl.setErrors(null);
    }
  }
}

export function dateValidator() {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const today = new Date().getTime();

    if (!(control && control.value)) {
      // if there's no control or no value, that's ok
      return null;
    }

    // return null if there's no errors
    return control.value.getTime() > today
      ? { invalidDate: 'You cannot use future dates' }
      : null;
  }
}
