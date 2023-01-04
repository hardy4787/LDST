import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { FormControlUtils } from '@ldst/utils';
import { CreateTimeSlot } from '../../models/create-time-slot.model';
import { PlaygroundStore } from '../../services/playground.store';
import * as moment from 'moment-timezone';

@UntilDestroy()
@Component({
  selector: 'ldst-create-time-slots-step',
  templateUrl: './create-time-slots-step.component.html',
  styleUrls: ['./create-time-slots-step.component.scss'],
})
export class CreateTimeSlotsStepComponent implements OnInit {
  @Input() form!: FormGroup;

  get priceControl(): AbstractControl {
    return this.form.controls['price'];
  }

  get gameTimeControl(): AbstractControl {
    return this.form.controls['gameTime'];
  }

  get daysCountControl(): AbstractControl {
    return this.form.controls['daysCount'];
  }

  get openPlaygroundTimeControl(): AbstractControl {
    return this.form.controls['openPlaygroundTime'];
  }

  get closePlaygroundTimeControl(): AbstractControl {
    return this.form.controls['closePlaygroundTime'];
  }

  constructor(private readonly playgroundStore: PlaygroundStore) {}

  ngOnInit(): void {
    this.form.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(() => this.playgroundStore.isTimeSlotsGenerated(false));
  }

  onGenerate(): void {
    this.playgroundStore.isTimeSlotsGenerated(false);
    if (this.form.invalid) {
      FormControlUtils.markAllAsTouched(this.form);
      return;
    }

    const slots = [];

    let startDay = 0;
    const date = new Date();
    date.setHours(6, 0, 0, 0);
    if (date.getTime() <= Date.now()) {
      startDay = 1;
    }
    for (
      let day = startDay;
      day < this.daysCountControl.value + startDay;
      day++
    ) {
      const nextDay = this.getNextDay(day);
      slots.push(...this.generateTimeSlots(nextDay));
    }

    this.playgroundStore.updateTimeSlots(slots);
    this.playgroundStore.isTimeSlotsGenerated(true);
  }

  private getNextDay(day: number): Date {
    const date = new Date();

    date.setHours(6, 0, 0, 0);
    date.setDate(date.getDate() + day);
    date.setMinutes(date.getMinutes() + this.openPlaygroundTimeControl.value);
    return date;
  }

  private generateTimeSlots(startTime: Date): CreateTimeSlot[] {
    const slots = [];
    let endTime = new Date(startTime);
    const iterations = Math.floor(
      (this.closePlaygroundTimeControl.value -
        this.openPlaygroundTimeControl.value) /
        this.gameTimeControl.value
    );

    for (let index = 0; index < iterations; index++) {
      endTime.setMinutes(startTime.getMinutes() + this.gameTimeControl.value);
      slots.push({
        price: this.priceControl.value,
        startTime: startTime,
        endTime: endTime,
      });
      startTime = new Date(endTime);
      endTime = new Date(endTime);
    }
    return slots;
  }
}
