import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationStatusService {
  private _authChangeSub$ = new BehaviorSubject<boolean>(false);
  authChanged = this._authChangeSub$.asObservable();

  constructor(private jwtHelper: JwtHelperService) {}

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem('token');

    return Boolean(token && !this.jwtHelper.isTokenExpired(token));
  };

  isUserAdmin = (): boolean => {
    const token = localStorage.getItem('token');

    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      const role =
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];
      return role === 'Administrator';
    }

    return false;
  };

  sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this._authChangeSub$.next(isAuthenticated);
  };

  logout = () => {
    localStorage.removeItem('token');
    this.sendAuthStateChangeNotification(false);
  };
}
