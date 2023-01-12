import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable } from 'rxjs';
import { CitySport } from '../models/city-sport.model';

@Injectable()
export class PlaygroundSearchService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  getCitySports$(countryId: number = 1): Observable<CitySport[]> {
    return this.httpClient.get<CitySport[]>(
      `${this.appConfig.baseURL}/Sports/citysports/${countryId}`
    );
  }
}
