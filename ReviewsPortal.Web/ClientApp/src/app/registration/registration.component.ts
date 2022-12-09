import {Component} from "@angular/core";
import {AbstractControl, FormGroup, FormControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent{
  error?: string;

  constructor(private http: HttpClient, private router: Router) {
  }

  checkPassword: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const confirm = group.get('confirm')?.value;
    const password = group.get('password')?.value;
    group.get('rememberMe')?.clearValidators();
    return password === confirm ? null : {notSame: true};
  }

  registrationForm = new FormGroup({
    'email': new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    'userName': new FormControl('', [
      Validators.required
    ]),
    'password': new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    'confirm': new FormControl('', [
      Validators.required
    ]),
    'rememberMe': new FormControl(false)},
    {validators: this.checkPassword})

  onSubmit() {
    this.http.post('api/user/registration', this.registrationForm.value).subscribe({
      next: _ => this.router.navigate(['/']),
      error: error => {
        if (error.status === 409) {
          this.error = "This name or email has already been created";
        }
      }
    });
  }
}
