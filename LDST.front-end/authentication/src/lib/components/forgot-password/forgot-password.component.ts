import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ValidationConstants } from '@ldst/shared';
import { ForgotPasswordParams } from '../../models/forgot-password-params.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'ldst-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent {
  form!: FormGroup;
  readonly validationConstants = ValidationConstants;

  get emailControl(): AbstractControl {
    return this.form.get('email') as AbstractControl;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router
  ) {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  onSubmit(): void {
    const body = {
      email: this.form.value.email,
      clientURI: 'http://localhost:4200/authentication/reset-password',
    } as ForgotPasswordParams;
    this.authService.forgotPassword$(body).subscribe(() => {
      this.snackBar.open(
        'The link has been sent, please check your email to reset your password.'
      );
      this.router.navigate(['/']);
    });
  }
}
