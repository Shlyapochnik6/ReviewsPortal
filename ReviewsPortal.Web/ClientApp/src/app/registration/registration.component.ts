import {Component} from "@angular/core";
import {AbstractControl, FormGroup, FormControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent{
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
      Validators.required,
      Validators.pattern("^[a-zA-Z0-9_]*$")
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
    console.log(this.registrationForm?.controls.rememberMe);
  }
}
