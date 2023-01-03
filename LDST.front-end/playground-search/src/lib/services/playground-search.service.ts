import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CitySport } from '../models/city-sport.model';

@Injectable()
export class PlaygroundSearchService {
  constructor(private readonly httpClient: HttpClient) {}

  getCitySports$(countryId: number = 1): Observable<CitySport[]> {
    return this.httpClient.get<CitySport[]>(
      `https://localhost:7286/Sports/citysports/${countryId}`
    );
  }
}
