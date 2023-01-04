import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from '../models/city.model';
import { CreatePlayground } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { Sport } from '../models/sport.model';

@Injectable()
export class CreatePlaygroundService {
  constructor(private readonly httpClient: HttpClient) {}

  createPlayground$(
    hostId: string,
    body: CreatePlayground
  ): Observable<number> {
    return this.httpClient.post<number>(
      `https://localhost:7286/Playgrounds/hosts/${hostId}`,
      body
    );
  }

  createTimeSlots$(
    playgroundId: number,
    body: CreateTimeSlot[]
  ): Observable<number> {
    return this.httpClient.post<number>(
      `https://localhost:7286/Playgrounds/${playgroundId}/timeslots`,
      body
    );
  }

  uploadTitleImage$(playgroundId: number, titleImage: File): Observable<void> {
    const data = new FormData();
    data.append('playgroundId', playgroundId.toString());
    data.append('titleImage', titleImage);
    return this.httpClient.post<void>(
      `https://localhost:7286/Playgrounds/title-image`,
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
      `https://localhost:7286/Playgrounds/gallery-images`,
      data
    );
  }

  getSports$(): Observable<Sport[]> {
    return this.httpClient.get<Sport[]>('https://localhost:7286/sports');
  }

  getCities$(): Observable<City[]> {
    return this.httpClient.get<City[]>('https://localhost:7286/locations/1');
  }
}
