import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { AuthenticationStatusService } from '../services/authentication-status.service';
@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authStatusService: AuthenticationStatusService,
    private router: Router
  ) {}
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authStatusService.isUserAuthenticated()) {
      console.log('AuthGuard', true);
      return true;
    }
    console.log('AuthGuard', false);
    this.router.navigate(['/authentication/login'], {
      queryParams: { returnUrl: state.url },
    });

    return false;
  }
}
