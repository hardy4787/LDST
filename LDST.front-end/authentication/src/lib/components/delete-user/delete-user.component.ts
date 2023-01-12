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
import { DeleteUserParams } from '../../models/delete-user-params.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'ldst-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.scss'],
})
export class DeleteUserComponent implements OnInit {
  readonly validationConstants = ValidationConstants;

  form!: FormGroup;

  get emailControl(): AbstractControl {
    return this.form.get('email') as AbstractControl;
  }

  get passwordControl(): AbstractControl {
    return this.form.get('password') as AbstractControl;
  }

  constructor(
    private readonly authService: AuthenticationService,
    private readonly router: Router,
    private readonly authStatusService: AuthenticationStatusService,
    private readonly notify: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  onSubmit(): void {
    const formValue = this.form.value;
    const userForAuth: DeleteUserParams = {
      email: formValue.email,
      password: formValue.password,
    };
    this.authService.deleteUser$(userForAuth).subscribe(() => {
      this.notify.open('User deleted successfully.');
      // localStorage.removeItem('token');
      // this.authStatusService.sendAuthStateChangeNotification(false);
      // this.router.navigate(['/']);
    });
  }
}
