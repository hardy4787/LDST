import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable()
export class TimeSlotsValidators {
  checkTimeInterval = (
    startTimeControl: AbstractControl,
    endTimeControl: AbstractControl,
    intervalControl: AbstractControl
  ): ValidatorFn => {
    return (): ValidationErrors | null => {
      if (!startTimeControl || !endTimeControl || !intervalControl) {
        return null;
      }

      const openPlaygroundTimeValue = startTimeControl.value;
      const closePlaygroundTimeValue = endTimeControl.value;
      const gameTimeValue = intervalControl.value;

      if (
        !openPlaygroundTimeValue ||
        !closePlaygroundTimeValue ||
        !gameTimeValue
      ) {
        return null;
      }

      const timeGap = closePlaygroundTimeValue - openPlaygroundTimeValue;
      if (timeGap < gameTimeValue) {
        return { timeGapIncorrect: true };
      }

      return null;
    };
  };
}
