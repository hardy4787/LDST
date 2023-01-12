import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationStatusService, ValidationConstants } from '@ldst/shared';
import { SignInParams } from '../../models/sign-in-params.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'ldst-login',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.scss'],
})
export class LoginUserComponent implements OnInit {
  readonly validationConstants = ValidationConstants;

  returnUrl!: string;
  loginForm!: FormGroup;

  get emailControl(): AbstractControl {
    return this.loginForm.get('email') as AbstractControl;
  }

  get passwordControl(): AbstractControl {
    return this.loginForm.get('password') as AbstractControl;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly authStatusService: AuthenticationStatusService,
    private readonly notify: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  onSave(): void {
    const formValue = this.loginForm.value;
    const userForAuth: SignInParams = {
      email: formValue.email,
      password: formValue.password,
    };
    this.authService
      .loginUser$(userForAuth)
      .subscribe(({ token, is2StepVerificationRequired, provider }) => {
        if (is2StepVerificationRequired) {
          this.notify.open('Verification code was sent to your email.');
          this.router.navigate(['/authentication/two-step-verification'], {
            queryParams: {
              returnUrl: this.returnUrl,
              provider: provider,
              email: userForAuth.email,
            },
          });
        } else {
          localStorage.setItem('token', token);
          this.authStatusService.sendAuthStateChangeNotification(true);
          this.router.navigate([this.returnUrl]);
        }
      });
  }
}
