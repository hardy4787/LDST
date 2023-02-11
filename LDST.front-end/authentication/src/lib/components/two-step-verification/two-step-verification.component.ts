import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AuthenticationService,
  AuthenticationStatusService,
  TwoFactorParams,
  ValidationConstants,
} from '@ldst/shared';

@Component({
  selector: 'ldst-two-step-verification',
  templateUrl: './two-step-verification.component.html',
  styleUrls: ['./two-step-verification.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TwoStepVerificationComponent implements OnInit {
  readonly validationConstants = ValidationConstants;

  provider!: string;
  email!: string;
  returnUrl!: string;

  form!: FormGroup;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly authStatusService: AuthenticationStatusService
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      twoFactorCode: new FormControl('', [Validators.required]),
    });

    this.provider = this.route.snapshot.queryParams['provider'];
    this.email = this.route.snapshot.queryParams['email'];
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
  }

  onSubmit(): void {
    const formValue = this.form.value;

    const params: TwoFactorParams = {
      email: this.email,
      provider: this.provider,
      token: formValue.twoFactorCode,
    };
    this.authService
      .twoFactorLogin$(params)
      .subscribe(({ token, userName, userId }) => {
        localStorage.setItem('token', token);
        localStorage.setItem('userName', userName);
        localStorage.setItem('userId', userId);
        this.authStatusService.sendAuthStateChangeNotification(true);
        this.router.navigate([this.returnUrl]);
      });
  }
}
