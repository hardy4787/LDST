import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable } from 'rxjs';
import { PlaygroundOverview } from '../models/playground-overview.model';

@Injectable()
export class PlaygroundOverviewService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  getPlaygroundOverview$(playgroundId: number): Observable<PlaygroundOverview> {
    return this.httpClient.get<PlaygroundOverview>(
      `${this.appConfig.baseURL}/Playgrounds/${playgroundId}`
    );
  }
}
