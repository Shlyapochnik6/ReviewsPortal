import {Injectable} from "@angular/core";
import {HttpInterceptor, HttpRequest, HttpHandler,
  HttpEvent, HttpErrorResponse} from '@angular/common/http';
import {tap, Observable} from "rxjs";
import {Router} from "@angular/router";

@Injectable()

export class AuthErrorsInterceptor implements HttpInterceptor {

  constructor(private router: Router) {
  }

  intercept(req: HttpRequest<any>,
            next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      tap({
        error: err => {
          if (err instanceof HttpErrorResponse){
            if (err.status === 401) {
              this.router.navigate(['/login']);
              return;
            }
            else if (err.status === 403) {
              this.router.navigate(['/login']);
              return;
            }
          }
        }
      })
    )
  }
}
