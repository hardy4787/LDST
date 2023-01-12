import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable } from 'rxjs';
import { City } from '../models/city.model';
import { CreatePlayground } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { Sport } from '../models/sport.model';

@Injectable()
export class CreatePlaygroundService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  createPlayground$(
    hostId: string,
    body: CreatePlayground
  ): Observable<number> {
    return this.httpClient.post<number>(
      `${this.appConfig.baseURL}/Playgrounds/hosts/${hostId}`,
      body
    );
  }

  createTimeSlots$(
    playgroundId: number,
    body: CreateTimeSlot[]
  ): Observable<number> {
    return this.httpClient.post<number>(
      `${this.appConfig.baseURL}/Playgrounds/${playgroundId}/timeslots`,
      body
    );
  }

  uploadTitleImage$(playgroundId: number, titleImage: File): Observable<void> {
    const data = new FormData();
    data.append('playgroundId', playgroundId.toString());
    data.append('titleImage', titleImage);
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Playgrounds/title-image`,
      data
    );
  }

  uploadGalleryImages$(playgroundId: number, images: File[]): Observable<void> {
    const data = new FormData();
    data.append('playgroundId', playgroundId.toString());
    for (let i = 0; i < images.length; i++) {
      data.append('galleryImages', images[i]);
    }
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Playgrounds/gallery-images`,
      data
    );
  }

  getSports$(): Observable<Sport[]> {
    return this.httpClient.get<Sport[]>(`${this.appConfig.baseURL}/sports`);
  }

  getCities$(): Observable<City[]> {
    return this.httpClient.get<City[]>(`${this.appConfig.baseURL}/locations/1`);
  }
}
