import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable } from 'rxjs';
import { Claim } from '../models/claim.model';

@Injectable()
export class PrivacyService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  getClaims$(): Observable<Claim[]> {
    return this.httpClient.get<Claim[]>(`${this.appConfig.baseURL}/Privacy`);
  }
}
