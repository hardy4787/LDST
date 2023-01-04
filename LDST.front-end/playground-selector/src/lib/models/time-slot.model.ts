export interface TimeSlot {
  id: number;
  price: number;
  startTime: Date | string;
  endTime: Date | string;
  gameTimeSlotStatus: number;
}
