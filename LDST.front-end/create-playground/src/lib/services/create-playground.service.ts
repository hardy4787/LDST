import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { City } from '../models/city.model';
import { CreatePlayground } from '../models/create-playground.model';
import { CreateTimeSlot } from '../models/create-time-slot.model';
import { PlaygroundTitleImage } from '../models/playground-title-image.model';
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

  uploadTitleImage$({
    titleImage,
    playgroundId,
  }: PlaygroundTitleImage): Observable<void> {
    const data = new FormData();
    data.append('titleImage', titleImage);
    data.append('playgroundId', playgroundId.toString());
    return this.httpClient.post<void>(
      `https://localhost:7286/Playgrounds/title-photo`,
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
