import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable, of } from 'rxjs';
import { PlaygroundView } from '../models/playground-view.model';

@Injectable()
export class PlaygroundSelectorService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  getPlayground$(
    cityId: number,
    sportId: number
  ): Observable<PlaygroundView[]> {
    return this.httpClient.get<PlaygroundView[]>(
      `${this.appConfig.baseURL}/Playgrounds/cities/${cityId}/sports/${sportId}`
    );
  }
}
