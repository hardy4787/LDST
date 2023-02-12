import { FormArray, FormControl, FormGroup } from '@angular/forms';

export class FormControlUtils {
  static markAllAsTouched = (
    formGroup: FormGroup | FormArray | FormControl,
    invalidOnly: boolean = true
  ): void => {
    if (formGroup instanceof FormControl) {
      if (!invalidOnly || (invalidOnly && formGroup.invalid))
        formGroup.markAsTouched({ onlySelf: true });
      return;
    }

    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        if (!invalidOnly || (invalidOnly && control.invalid)) {
          control.markAsTouched({ onlySelf: true });
        }
      } else if (control instanceof FormArray || control instanceof FormGroup) {
        FormControlUtils.markAllAsTouched(control, invalidOnly);
      }
    });
  };

  static markAllAsDirty = (
    formGroup: FormGroup | FormArray | FormControl,
    invalidOnly: boolean = true
  ): void => {
    if (formGroup instanceof FormControl) {
      if (!invalidOnly || (invalidOnly && formGroup.invalid))
        formGroup.markAsDirty({ onlySelf: true });
      return;
    }

    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        if (!invalidOnly || (invalidOnly && control.invalid)) {
          control.markAsDirty({ onlySelf: true });
        }
      } else if (control instanceof FormArray || control instanceof FormGroup) {
        FormControlUtils.markAllAsDirty(control, invalidOnly);
      }
    });
  };
}
