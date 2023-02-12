import { Sport } from './sport.model';

export interface CitySport {
  cityId: number;
  cityName: string;
  sports: Sport[];
}
