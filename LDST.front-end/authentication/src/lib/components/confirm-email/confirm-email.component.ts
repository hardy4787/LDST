import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfirmEmailParams } from '../../models/email-confirm-params.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'ldst-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss'],
})
export class ConfirmEmailComponent implements OnInit {
  showSuccess!: boolean;
  showError!: boolean;
  errorMessage!: string;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.confirmEmail();
  }

  private confirmEmail = () => {
    this.showError = this.showSuccess = false;
    const token = this.route.snapshot.queryParams['token'];
    const email = this.route.snapshot.queryParams['email'];

    this.authService
      .confirmEmail$({ token, email } as ConfirmEmailParams)
      .subscribe({
        next: (_) => (this.showSuccess = true),
        error: (err: HttpErrorResponse) => {
          this.showError = true;
          this.errorMessage = err.message;
        },
      });
  };
}
