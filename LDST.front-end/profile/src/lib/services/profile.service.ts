import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable, of, tap, timer } from 'rxjs';
import { Profile } from '../models/profile.model';
import { UpdateProfileParams } from '../models/update-profile-params.model';
import { UpdateProfileResponse } from '../models/update-profile-response.model';

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

  updateProfile$(body: UpdateProfileParams): Observable<UpdateProfileResponse> {
    return this.httpClient.put<UpdateProfileResponse>(
      `${this.appConfig.baseURL}/Profile/`,
      body
    );
  }

  updateTitleImage$(file: File | null, userName: string): Observable<void> {
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
