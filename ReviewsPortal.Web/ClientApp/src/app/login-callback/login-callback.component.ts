import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthService} from "../../common/services/user/auth.service";

@Component({
  selector: 'app-login-callback',
  templateUrl: './login-callback.component.html',
  styleUrls: ['./login-callback.component.css']
})

export class LoginCallbackComponent {
  error?: string;

  constructor(private http: HttpClient, private authService: AuthService,
              private router: Router) {
    this.http.get('api/user/external-login-callback').subscribe({
      next: () => {
        this.authService.isAuthenticated = true;
        this.router.navigate(['/']);
      },
      error: error => this.error = error
    });
  }
}
