import { Pipe, PipeTransform } from '@angular/core';
import { DaySchedule } from '@ldst/shared';

@Pipe({
  name: 'dayScheduleFormat',
})
export class DaySchedulePipe implements PipeTransform {
  readonly weekDays = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];
  transform({
    isClosed,
    openingTime,
    dayOfWeek,
    closingTime,
  }: DaySchedule): string {
    const result = this.weekDays[dayOfWeek] + ': ';
    if (isClosed) {
      return result + 'Closed';
    }

    return (
      result + openingTime?.slice(0, -3) + ' - ' + closingTime?.slice(0, -3)
    );
  }
}
