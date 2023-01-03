import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable()
export class TitleImageValidators {
  readonly acceptedFormats = [
    'image/gif',
    'image/jpg',
    'image/jpeg',
    'image/png',
  ];

  public checkSize(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control) {
        return null;
      }

      const value = control.value as File;
      if (!value) {
        return null;
      }

      if (value.size > 5242880) {
        return { exceedSize: true };
      }

      if (value.size === 0) {
        return { zeroSize: true };
      }

      return null;
    };
  }

  public checkFormat(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control) {
        return null;
      }

      const value = control.value as File;
      if (!value) {
        return null;
      }

      if (!this.acceptedFormats.some((type) => value.type.includes(type))) {
        return { incorrectType: true };
      }

      return null;
    };
  }
}
