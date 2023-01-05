import {Injectable} from "@angular/core";
import {CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {AuthService} from "../services/user/auth.service";

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private http: HttpClient,
              private router: Router,
              private authService: AuthService) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.authService.checkIsAuthenticated()
      .subscribe({
        next: value => {
          if (!value) {
            this.router.navigate(['/login']);
          }
        }
      });
    return this.authService.checkIsAuthenticated();
  }
}
