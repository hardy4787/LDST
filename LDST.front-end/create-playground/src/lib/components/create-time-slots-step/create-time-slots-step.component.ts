import {
  ChangeDetectionStrategy,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { FormControlUtils } from '@ldst/utils';
import { CreateTimeSlot } from '../../models/create-time-slot.model';
import { PlaygroundStore } from '../../services/playground.store';
import { DayScheduleView } from '../../models/day-schedule-view.model';
import { DaySchedule } from '@ldst/shared';

@UntilDestroy()
@Component({
  selector: 'ldst-create-time-slots-step',
  templateUrl: './create-time-slots-step.component.html',
  styleUrls: ['./create-time-slots-step.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateTimeSlotsStepComponent implements OnInit {
  @Input() timeSlotsConfigurationControl!: FormGroup;
  @Input() weekScheduleControl!: FormArray;

  markDaysAsTouched = false;

  get priceControl(): AbstractControl {
    return this.timeSlotsConfigurationControl.controls['price'];
  }

  get gameTimeControl(): AbstractControl {
    return this.timeSlotsConfigurationControl.controls['gameTime'];
  }

  get daysCountControl(): AbstractControl {
    return this.timeSlotsConfigurationControl.controls['daysCount'];
  }

  constructor(private readonly playgroundStore: PlaygroundStore) {}

  dayOfWeeksTrackBy = (_: number, details: any): number => details.dayOfWeek;

  ngOnInit(): void {
    this.timeSlotsConfigurationControl.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.playgroundStore.isTimeSlotsGenerated(false);
      });
  }

  onGenerate(): void {
    this.playgroundStore.isTimeSlotsGenerated(false);
    if (this.timeSlotsConfigurationControl.invalid) {
      FormControlUtils.markAllAsTouched(this.timeSlotsConfigurationControl);
      this.markDaysAsTouched = true;
      return;
    }
    const daysMap = this.generateDaysOfWeekMap();

    const startDay = this.getStartDay(daysMap);

    const slots = [];
    for (
      let daysAfter = startDay;
      daysAfter < this.daysCountControl.value;
      daysAfter++
    ) {
      const date = new Date();
      date.setDate(date.getDate() + daysAfter);
      const weekDay = date.getDay();
      const dayMap = daysMap.get(weekDay) as DayScheduleView;
      if (dayMap.isClosed) {
        continue;
      }

      slots.push(...this.generateTimeSlots(dayMap, date));
    }

    this.playgroundStore.updateTimeSlots(slots);
    this.playgroundStore.updateWeekSchedule(
      this.weekScheduleControl.controls?.map((control, i) => {
        const { isClosed, openingTime, closingTime } = (control as FormGroup)
          .controls;

        return {
          isClosed: isClosed.value,
          openingTime: openingTime.value,
          closingTime: closingTime.value,
          dayOfWeek: i,
        } as DaySchedule;
      })
    );
    this.playgroundStore.isTimeSlotsGenerated(true);
  }

  private getStartDay(daysMap: Map<number, DayScheduleView>): number {
    const currentDay = 0;
    const nextDay = 1;
    const date = new Date();
    const dayOfWeek = date.getDay();
    const { openingHours, openingMinutes, isClosed } = daysMap.get(
      dayOfWeek
    ) as DayScheduleView;

    if (isClosed) {
      return currentDay;
    }

    date.setHours(openingHours);
    date.setMinutes(openingMinutes);

    if (date.getTime() <= Date.now()) {
      return nextDay;
    }

    return currentDay;
  }

  private generateDaysOfWeekMap(): Map<number, DayScheduleView> {
    const daysMap = new Map<number, DayScheduleView>();
    this.weekScheduleControl.controls?.forEach((control, index) => {
      const { openingTime, closingTime, isClosed } = (control as FormGroup)
        .controls;

      if (isClosed.value) {
        daysMap.set(index, {
          isClosed: isClosed.value,
        } as DayScheduleView);
        return;
      }

      const [, openingHours, openingMinutes] =
        openingTime.value.match(/^(\d+):(\d+)$/);
      const [, closingHours, closingMinutes] =
        closingTime.value.match(/^(\d+):(\d+)$/);
      daysMap.set(index, {
        isClosed: isClosed.value,
        openingHours: Number(openingHours),
        openingMinutes: Number(openingMinutes),
        closingHours: Number(closingHours),
        closingMinutes: Number(closingMinutes),
      });
    });

    return daysMap;
  }

  private generateTimeSlots(
    dayMap: DayScheduleView,
    date: Date
  ): CreateTimeSlot[] {
    const { openingHours, openingMinutes } = dayMap;

    let startTime = this.getStartOfWorkDay(openingHours, openingMinutes, date);
    let endTime = new Date(startTime);
    const slotsCount = this.getSlotsCountPerDay(dayMap);

    const slots = [];
    for (let i = 0; i < slotsCount; i++) {
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

  private getSlotsCountPerDay({
    openingHours,
    closingHours,
    openingMinutes,
    closingMinutes,
  }: DayScheduleView): number {
    const isSlotsSpan2Days = closingHours - openingHours < 0;
    const hoursInDay = 24;
    const timeFromOpenToClose = isSlotsSpan2Days
      ? hoursInDay -
        openingHours +
        closingHours +
        (closingMinutes - openingMinutes)
      : closingHours - openingHours + (closingMinutes - openingMinutes);
    const minutesInHour = 60;
    return Math.floor(
      timeFromOpenToClose / (this.gameTimeControl.value / minutesInHour)
    );
  }

  private getStartOfWorkDay(
    openingHours: number,
    openingMinutes: number,
    date: Date
  ) {
    const startTate = new Date(date);

    startTate.setHours(0, 0, 0, 0);
    startTate.setHours(startTate.getHours() + openingHours);
    startTate.setMinutes(startTate.getMinutes() + openingMinutes);
    return startTate;
  }
}
