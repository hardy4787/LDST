import { TimeSlot } from "./time-slot.model";

export interface Playground {
  id: number;
  name: string;
  averageRating: number;
  titlePhotoPath: string;
  timeSlots: TimeSlot[];
}
