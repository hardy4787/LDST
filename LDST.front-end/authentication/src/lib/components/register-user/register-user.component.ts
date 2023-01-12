import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SignUpParams } from '../../models/sign-up-params.model';
import { AuthenticationService } from '../../services/authentication.service';
import { ValidationConstants } from '@ldst/shared';
import { PasswordValidators } from '../../services/password.validators';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'ldst-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss'],
})
export class RegisterUserComponent {
  registerForm!: FormGroup;
  readonly passwordTextLimit = 50;
  readonly firstNameTextLimit = 50;
  readonly lastNameTextLimit = 50;
  readonly validationConstants = ValidationConstants;

  get emailControl(): AbstractControl {
    return this.registerForm.get('email') as AbstractControl;
  }

  get firstNameControl(): AbstractControl {
    return this.registerForm.get('firstName') as AbstractControl;
  }

  get lastNameControl(): AbstractControl {
    return this.registerForm.get('lastName') as AbstractControl;
  }

  get passwordControl(): AbstractControl {
    return this.registerForm.get('password') as AbstractControl;
  }

  get confirmPasswordControl(): AbstractControl {
    return this.registerForm.get('confirmPassword') as AbstractControl;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly passwordValidators: PasswordValidators,
    private readonly router: Router,
    private readonly notify: MatSnackBar
  ) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.firstNameTextLimit),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.lastNameTextLimit),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.passwordTextLimit),
      ]),
      confirmPassword: new FormControl(''),
    });

    this.confirmPasswordControl.addValidators(
      this.passwordValidators.checkEquality(
        this.passwordControl,
        this.confirmPasswordControl
      )
    );
  }

  onSave(): void {
    const formValues = this.registerForm.value;
    const user: SignUpParams = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirmPassword,
      clientURI: 'http://localhost:4200/authentication/confirm-email',
    };

    this.authService.registerUser$(user).subscribe(() => {
      this.notify.open('User is registered successfully.');
      this.notify.open(
        'You should confirm your email. Check your email please.'
      );
      this.router.navigate(['/authentication/login']);
    });
  }
}
