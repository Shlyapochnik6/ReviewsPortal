import {Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  error?: string;

  constructor(private http: HttpClient, private router: Router) {
  }

  loginForm: FormGroup = new FormGroup({
    'email': new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    'password': new FormControl('', [
      Validators.required
    ]),
    'rememberMe': new FormControl(true)
  });

  onSubmit(){
    console.log(this.loginForm.value)
    this.http.post('api/user/login', this.loginForm.value)
      .subscribe({
        next: _ => this.router.navigate(['/']),
        error: err => {
          if (err.status === 401)
            this.error = 'The entered password is not consistent with the user password!'
          if (err.status === 404)
            this.error = 'The entered user was not found!'
        }
      })
  }
}
