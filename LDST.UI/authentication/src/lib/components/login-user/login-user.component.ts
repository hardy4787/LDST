import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AuthenticationService,
  AuthenticationStatusService,
  SignInParams,
  ValidationConstants,
} from '@ldst/shared';
import { ToastrService } from 'ngx-toastr';

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
    private readonly toastr: ToastrService
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
      .subscribe(
        ({
          token,
          userName,
          userId,
          is2StepVerificationRequired,
          provider,
        }) => {
          if (is2StepVerificationRequired) {
            this.toastr.info('Verification code was sent to your email.');
            this.router.navigate(['/authentication/two-step-verification'], {
              queryParams: {
                returnUrl: this.returnUrl,
                provider: provider,
                email: userForAuth.email,
              },
            });
          } else {
            localStorage.setItem('token', token);
            localStorage.setItem('userName', userName);
            localStorage.setItem('userId', userId);
            this.authStatusService.sendAuthStateChangeNotification(true);
            this.router.navigate([this.returnUrl]);
          }
        }
      );
  }
}
