import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PlaygroundView } from '../models/playground-view.model';
import { Playground } from '../models/playground.model';

@Injectable()
export class PlaygroundSelectorService {
  constructor(private readonly httpClient: HttpClient) {}

  getPlayground$(
    cityId: number,
    sportId: number
  ): Observable<PlaygroundView[]> {
    return this.httpClient.get<PlaygroundView[]>(
      `https://localhost:7286/Playgrounds/cities/${cityId}/sports/${sportId}`
    );
  }
}
