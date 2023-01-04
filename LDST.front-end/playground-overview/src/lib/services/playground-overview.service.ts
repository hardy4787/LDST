import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PlaygroundOverview } from '../models/playground-overview.model';

@Injectable()
export class PlaygroundOverviewService {
  constructor(private readonly httpClient: HttpClient) {}

  getPlaygroundOverview$(playgroundId: number): Observable<PlaygroundOverview> {
    return this.httpClient.get<PlaygroundOverview>(
      `https://localhost:7286/Playgrounds/${playgroundId}`
    );
  }
}
