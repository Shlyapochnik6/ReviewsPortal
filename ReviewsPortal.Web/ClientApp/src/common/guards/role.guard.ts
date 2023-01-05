import {Injectable} from "@angular/core";
import {CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthService} from "../services/user/auth.service";

@Injectable({
  providedIn: 'root'
})

export class RoleGuard implements CanActivate {

  constructor(private http: HttpClient,
              private router: Router,
              private authService: AuthService) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.authService.getUserRole()
      .subscribe({
        next: value => {
          if (!value) {
            this.router.navigate(['/']);
          }
        }
      })
    return this.authService.getUserRole();
  }
}
