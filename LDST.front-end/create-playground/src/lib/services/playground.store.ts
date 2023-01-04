import { Injectable } from '@angular/core';
import { CreatePlayground } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { ComponentStore } from '@ngrx/component-store';
import { ImageInfo } from '../models/image-info.model';

export interface PlaygroundState {
  playgroundInfo: CreatePlayground;
  titleImage: File | null;
  galleryImages: ImageInfo[];
  timeSlots: CreateTimeSlot[];
  isTimeSlotsGenerated: boolean;
}

const initialState: PlaygroundState = {
  playgroundInfo: {} as CreatePlayground,
  galleryImages: [],
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
  readonly galleryImages$ = this.select((state) => state.galleryImages);
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
