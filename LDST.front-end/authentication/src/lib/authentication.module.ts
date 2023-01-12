import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { RouterModule } from '@angular/router';
import { AuthGuard, SharedModule } from '@ldst/shared';
import { PasswordValidators } from './services/password.validators';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
import { TwoStepVerificationComponent } from './components/two-step-verification/two-step-verification.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      { path: 'register', component: RegisterUserComponent },
      { path: 'login', component: LoginUserComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'confirm-email', component: ConfirmEmailComponent },
      {
        path: 'delete-user',
        component: DeleteUserComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'two-step-verification',
        component: TwoStepVerificationComponent,
      },
    ]),
  ],
  declarations: [
    RegisterUserComponent,
    LoginUserComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent,
    DeleteUserComponent,
    TwoStepVerificationComponent,
  ],
  providers: [PasswordValidators],
})
export class AuthenticationModule {}
