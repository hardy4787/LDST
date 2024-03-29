import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private readonly toastr: ToastrService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        const errorMessage = this.handleError(error);
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  private handleError = (error: HttpErrorResponse): string => {
    if (error.status === 404) {
      return this.handleNotFound(error);
    } else if (error.status === 401) {
      return this.handleUnauthorized(error);
    } else if (error.status === 403) {
      return this.handleForbidden(error);
    }
    return this.handleBadRequest(error);
  };

  private handleForbidden = (error: HttpErrorResponse) => {
    this.router.navigate(['/forbidden'], {
      queryParams: { returnUrl: this.router.url },
    });
    return 'Forbidden';
  };

  private handleUnauthorized = (error: HttpErrorResponse) => {
    if (this.router.url === '/authentication/login') {
      return 'Authentication failed. Wrong Username or Password';
    } else {
      this.router.navigate(['/authentication/login'], {
        queryParams: { returnUrl: this.router.url },
      });
      return error.message;
    }
  };

  private handleNotFound = (error: HttpErrorResponse): string => {
    this.router.navigate(['/404']);
    return error.message;
  };

  private handleBadRequest = (error: HttpErrorResponse): string => {
    if (error.error?.errors) {
      const values = Object.values(error.error.errors) as string[];
      values.forEach((errorMessage) => this.toastr.error(errorMessage));

      let message = '';
      values.map((m: string) => {
        message += m + '<br>';
      });
      return message.slice(0, -4);
    }

    if (error.error) {
      this.toastr.error(error.error.title);

      return error.error;
    }

    return error.message;
  };
}
