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
export class AdminGuard implements CanActivate {
  constructor(
    private authStatusService: AuthenticationStatusService,
    private router: Router
  ) {}
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authStatusService.isUserAdmin()) return true;

    this.router.navigate(['/forbidden'], {
      queryParams: { returnUrl: state.url },
    });
    return false;
  }
}
