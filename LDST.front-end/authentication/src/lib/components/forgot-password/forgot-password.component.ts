import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import {
  AuthenticationService,
  ForgotPasswordParams,
  ValidationConstants,
} from '@ldst/shared';
import { ToastrService } from 'ngx-toastr';

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
    private readonly toastr: ToastrService,
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
      this.toastr.info(
        'The link has been sent, please check your email to reset your password.'
      );
      this.router.navigate(['/']);
    });
  }
}
