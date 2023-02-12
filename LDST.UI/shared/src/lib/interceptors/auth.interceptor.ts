import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';

export class AuthInterceptorService implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const userName = localStorage.getItem('userName');

    if (userName) {
      request = request.clone({
        setHeaders: {
          userName: userName,
        },
      });
    }

    return next.handle(request);
  }
}
