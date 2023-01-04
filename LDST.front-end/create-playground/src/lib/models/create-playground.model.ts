import { WeekSchedule } from '@ldst/shared';

export interface PlaygroundInfo {
  name: string;
  descriptions: string;
  sportId: number;
  address1: string;
  address2: string;
  cityId: number;
  state: string;
  zipPhoto: string;
}

export interface CreatePlayground extends PlaygroundInfo {
  weekSchedule: WeekSchedule;
}
