import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/components/Auth/services/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private readonly _authService:AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    request = request.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${this._authService.token}`,
        'Content-Type': 'application/json'
      })
    })
    return next.handle(request);
  }
}
