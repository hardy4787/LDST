import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { FormControlUtils } from '@ldst/utils';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'ldst-day-schedule',
  templateUrl: './day-schedule.component.html',
  styleUrls: ['./day-schedule.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DayScheduleComponent implements OnInit {
  _form!: FormGroup;

  get form(): FormGroup {
    return this._form;
  }

  @Input() set formInput(value: AbstractControl) {
    this._form = value as FormGroup;
  }

  @Input() set markAllAsTouched(value: boolean) {
    if (value) {
      FormControlUtils.markAllAsTouched(this._form);
    }
  }

  get openingTimeControl(): FormControl {
    return this.form.get('openingTime') as FormControl;
  }

  get closingTimeControl(): FormControl {
    return this.form.get('closingTime') as FormControl;
  }

  get isClosedControl(): FormControl {
    return this.form.get('isClosed') as FormControl;
  }

  ngOnInit(): void {
    this.form.addControl(
      'openingTime',
      new FormControl('', Validators.required)
    );
    this.form.addControl(
      'closingTime',
      new FormControl('', Validators.required)
    );
    this.form.addControl('isClosed', new FormControl(false));

    this.isClosedControl.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((value) => {
        if (value) {
          this.openingTimeControl.disable();
          this.closingTimeControl.disable();
        } else {
          this.openingTimeControl.enable();
          this.closingTimeControl.enable();
        }
      });
  }
}
