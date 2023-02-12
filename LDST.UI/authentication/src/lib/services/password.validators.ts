import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable()
export class PasswordValidators {
  public checkEquality = (
    password: AbstractControl,
    confirmPassword: AbstractControl
  ): ValidatorFn => {
    return (): ValidationErrors | null => {
      if (!password || !confirmPassword) {
        return null;
      }

      const passwordValue = password.value;
      const confirmPasswordValue = confirmPassword.value;

      if (!passwordValue) {
        return null;
      }

      if (passwordValue !== confirmPasswordValue) {
        return { notEqual: true };
      }

      return null;
    };
  };
}
