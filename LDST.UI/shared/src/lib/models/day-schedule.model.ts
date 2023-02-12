export interface DaySchedule {
  isClosed: boolean;
  openingTime: string | null;
  closingTime: string | null;
  dayOfWeek: number;
}
