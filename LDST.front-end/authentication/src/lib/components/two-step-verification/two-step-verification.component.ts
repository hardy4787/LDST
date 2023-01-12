import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationStatusService, ValidationConstants } from '@ldst/shared';
import { SignInResponse } from '../../models/sign-in-response.model';
import { TwoFactorParams } from '../../models/two-factor-params.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'ldst-two-step-verification',
  templateUrl: './two-step-verification.component.html',
  styleUrls: ['./two-step-verification.component.scss'],
})
export class TwoStepVerificationComponent implements OnInit {
  readonly validationConstants = ValidationConstants;

  provider!: string;
  email!: string;
  returnUrl!: string;

  form!: FormGroup;

  constructor(
    private authService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
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
      .subscribe((res: SignInResponse) => {
        localStorage.setItem('token', res.token);
        this.authStatusService.sendAuthStateChangeNotification(true);
        this.router.navigate([this.returnUrl]);
      });
  }
}
