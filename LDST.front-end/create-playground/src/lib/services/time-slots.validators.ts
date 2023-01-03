import { Injectable } from '@angular/core';
import { FormGroup, ValidatorFn } from '@angular/forms';

@Injectable()
export class TimeSlotsValidators {
  public checkTimeSlotsGap(): ValidatorFn | any {
    return (formGroup: FormGroup) => {
      const openPlaygroundTimeControl = formGroup.get('openPlaygroundTime');
      const closePlaygroundTimeControl = formGroup.get('closePlaygroundTime');
      const gameTimeControl = formGroup.get('gameTime');

      if (
        !openPlaygroundTimeControl ||
        !closePlaygroundTimeControl ||
        !gameTimeControl
      ) {
        return null;
      }

      const openPlaygroundTimeValue = openPlaygroundTimeControl.value;
      const closePlaygroundTimeValue = closePlaygroundTimeControl.value;
      const gameTimeValue = gameTimeControl.value;

      if (
        !openPlaygroundTimeValue ||
        !closePlaygroundTimeValue ||
        !gameTimeValue
      ) {
        return null;
      }

      const timeGap = closePlaygroundTimeValue - openPlaygroundTimeValue;
      if (timeGap < gameTimeValue) {
        return { timeGapIncorrect: true }; // This is our error!
      }

      return null;
    };
  }
}
