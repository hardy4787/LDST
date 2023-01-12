import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidationConstants } from '@ldst/shared';
import { ResetPasswordParams } from '../../models/reset-password-params.model';
import { AuthenticationService } from '../../services/authentication.service';
import { PasswordValidators } from '../../services/password.validators';

@Component({
  selector: 'ldst-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent {
  form!: FormGroup;
  readonly passwordTextLimit = 50;
  readonly validationConstants = ValidationConstants;

  get passwordControl(): AbstractControl {
    return this.form.get('password') as AbstractControl;
  }

  get confirmPasswordControl(): AbstractControl {
    return this.form.get('confirmPassword') as AbstractControl;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly passwordValidators: PasswordValidators,
    private readonly route: ActivatedRoute,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router
  ) {
    this.form = new FormGroup({
      password: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.passwordTextLimit),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
    });

    this.confirmPasswordControl.addValidators(
      this.passwordValidators.checkEquality(
        this.passwordControl,
        this.confirmPasswordControl
      )
    );
  }

  onSubmit(): void {
    const formValues = this.form.value;
    const body: ResetPasswordParams = {
      password: formValues.password,
      confirmPassword: formValues.confirmPassword,
      token: this.route.snapshot.queryParams['token'],
      email: this.route.snapshot.queryParams['email'],
    };

    this.authService.resetPassword$(body).subscribe(() => {
      this.snackBar.open('Your password has been reset.');
      this.router.navigate(['/authentication/login']);
    });
  }
}
