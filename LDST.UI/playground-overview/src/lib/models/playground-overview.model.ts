import { WeekSchedule } from '@ldst/shared';
export interface PlaygroundOverview {
  id: number;
  name: string;
  description: string;
  averageRating: number;
  titlePhotoPath: string;
  photoPaths: string[];
  address1: string;
  address2: string;
  state: string;
  zipCode: string;
  city: string;
  weekSchedule: WeekSchedule;
}
