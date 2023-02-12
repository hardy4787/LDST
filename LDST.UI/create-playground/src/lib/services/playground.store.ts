import { Injectable } from '@angular/core';
import { PlaygroundInfo } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { ComponentStore } from '@ngrx/component-store';
import { ImageInfo } from '../../../../shared/src/lib/models/image-info.model';
import { DaySchedule } from '@ldst/shared';
export interface PlaygroundState {
  playgroundInfo: PlaygroundInfo;
  titleImage: File | null;
  galleryImages: ImageInfo[];
  timeSlots: CreateTimeSlot[];
  weekSchedule: DaySchedule[];
  isTimeSlotsGenerated: boolean;
}

const initialState: PlaygroundState = {
  playgroundInfo: {} as PlaygroundInfo,
  galleryImages: [],
  titleImage: {} as File,
  timeSlots: [] as CreateTimeSlot[],
  weekSchedule: [] as DaySchedule[],
  isTimeSlotsGenerated: false,
};

@Injectable()
export class PlaygroundStore extends ComponentStore<PlaygroundState> {
  constructor() {
    super(initialState);
  }

  readonly weekSchedule$ = this.select((state) => state.weekSchedule);
  readonly playgroundInfo$ = this.select((state) => state.playgroundInfo);
  readonly titleImage$ = this.select((state) => state.titleImage);
  readonly galleryImages$ = this.select((state) => state.galleryImages);
  readonly timeSlots$ = this.select((state) => state.timeSlots);
  readonly isTimeSlotsGenerated$ = this.select(
    (state) => state.isTimeSlotsGenerated
  );

  readonly updatePlaygroundInfo = this.updater(
    (state, playgroundInfo: PlaygroundInfo) => ({
      ...state,
      playgroundInfo,
    })
  );

  readonly updateTitleImage = this.updater((state, titleImage: File) => ({
    ...state,
    titleImage,
  }));

  readonly resetTitleImage = this.updater((state) => ({
    ...state,
    titleImage: null,
  }));

  readonly updateTimeSlots = this.updater(
    (state, timeSlots: CreateTimeSlot[]) => ({
      ...state,
      timeSlots,
    })
  );

  readonly updateWeekSchedule = this.updater(
    (state, weekSchedule: DaySchedule[]) => ({
      ...state,
      weekSchedule,
    })
  );

  readonly addGalleryImage = this.updater((state, galleryImage: ImageInfo) => ({
    ...state,
    galleryImages: [...state.galleryImages, galleryImage],
  }));

  readonly removeGalleryImage = this.updater((state, id: string) => ({
    ...state,
    galleryImages: state.galleryImages.filter((i) => i.id !== id),
  }));

  readonly isTimeSlotsGenerated = this.updater(
    (state, isTimeSlotsGenerated: boolean) => ({
      ...state,
      isTimeSlotsGenerated,
    })
  );
}
