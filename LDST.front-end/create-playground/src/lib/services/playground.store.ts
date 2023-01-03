import { Injectable } from '@angular/core';
import { CreatePlayground } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { ComponentStore } from '@ngrx/component-store';

export interface PlaygroundState {
  playgroundInfo: CreatePlayground;
  titleImage: File | null;
  timeSlots: CreateTimeSlot[];
  isTimeSlotsGenerated: boolean;
}

const initialState: PlaygroundState = {
  playgroundInfo: {} as CreatePlayground,
  titleImage: {} as File,
  timeSlots: [] as CreateTimeSlot[],
  isTimeSlotsGenerated: false,
};

@Injectable()
export class PlaygroundStore extends ComponentStore<PlaygroundState> {
  constructor() {
    super(initialState);
  }

  readonly playgroundInfo$ = this.select((state) => state.playgroundInfo);
  readonly titleImage$ = this.select((state) => state.titleImage);
  readonly timeSlots$ = this.select((state) => state.timeSlots);
  readonly isTimeSlotsGenerated$ = this.select(
    (state) => state.isTimeSlotsGenerated
  );

  readonly updatePlaygroundInfo = this.updater(
    (state, playgroundInfo: CreatePlayground) => ({
      ...state,
      playgroundInfo,
    })
  );

  readonly updateTitleImage = this.updater((state, titleImage: File) => ({
    ...state,
    titleImage,
  }));

  readonly clearTitleImage = this.updater((state) => ({
    ...state,
    titleImage: null,
  }));

  readonly updateTimeSlots = this.updater(
    (state, timeSlots: CreateTimeSlot[]) => ({
      ...state,
      timeSlots,
    })
  );

  readonly isTimeSlotsGenerated = this.updater(
    (state, isTimeSlotsGenerated: boolean) => ({
      ...state,
      isTimeSlotsGenerated,
    })
  );
}
