import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@ldst/shared';
import { Observable } from 'rxjs';
import { DeleteUserParams } from '../models/delete-user-params.model';
import { ConfirmEmailParams } from '../models/email-confirm-params.model';
import { ForgotPasswordParams } from '../models/forgot-password-params.model';
import { ResetPasswordParams } from '../models/reset-password-params.model';
import { SignInParams } from '../models/sign-in-params.model';
import { SignInResponse } from '../models/sign-in-response.model';
import { SignUpParams } from '../models/sign-up-params.model';
import { TwoFactorParams } from '../models/two-factor-params.model';
import { Inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(
    @Inject(APP_CONFIG) private readonly appConfig: AppConfig,
    private httpClient: HttpClient
  ) {}

  registerUser$(body: SignUpParams): Observable<void> {
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Authentication/register`,
      body
    );
  }

  loginUser$(body: SignInParams): Observable<SignInResponse> {
    return this.httpClient.post<SignInResponse>(
      `${this.appConfig.baseURL}/Authentication/login`,
      body
    );
  }

  twoFactorLogin$(body: TwoFactorParams): Observable<SignInResponse> {
    return this.httpClient.post<SignInResponse>(
      `${this.appConfig.baseURL}/Authentication/login/two-factor`,
      body
    );
  }

  forgotPassword$(body: ForgotPasswordParams): Observable<void> {
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Authentication/forgot-password`,
      body
    );
  }

  resetPassword$(body: ResetPasswordParams): Observable<void> {
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Authentication/reset-password`,
      body
    );
  }

  confirmEmail$(body: ConfirmEmailParams): Observable<void> {
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Authentication/confirm-email`,
      body
    );
  }

  deleteUser$(body: DeleteUserParams): Observable<void> {
    return this.httpClient.post<void>(
      `${this.appConfig.baseURL}/Authentication/delete`,
      body
    );
  }
}
