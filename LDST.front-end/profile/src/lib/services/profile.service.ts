import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable, of, tap, timer } from 'rxjs';
import { Profile } from '../models/profile.model';
import { UpdateProfileParams } from '../models/update-profile-params.model';

@Injectable()
export class ProfileService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private readonly httpClient: HttpClient
  ) {}

  getProfile$(userName: string): Observable<Profile> {
    return this.httpClient.get<Profile>(
      `${this.appConfig.baseURL}/Profile/${userName}`
    );
  }

  updateProfile$(body: UpdateProfileParams): Observable<void> {
    return this.httpClient.put<void>(
      `${this.appConfig.baseURL}/Profile/`,
      body
    );
  }

  updateTitleImage$(file: File, userName: string): Observable<void> {
    const data = new FormData();
    data.append('userName', userName);
    if (file) {
      data.append('titleImage', file);
    } else {
      data.append('titleImage', '');
    }

    return this.httpClient.put<void>(
      `${this.appConfig.baseURL}/Profile/title-image`,
      data
    );
  }
}
