import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { filter, of, switchMap } from 'rxjs';
import { ProfileService } from './services/profile.service';
import { ProfileInfoParams } from './models/profile-info-params';
import { Profile } from './models/profile.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogParams } from '@ldst/organisms';
import {
  AuthenticationService,
  AuthenticationStatusService,
} from '@ldst/shared';
import { ToastrService } from 'ngx-toastr';
import { Location } from '@angular/common';

@UntilDestroy()
@Component({
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss'],
})
export class ProfilePageComponent implements OnInit {
  isCurrentUser = false;
  titleImageChanged = false;
  userName!: string;
  profile!: Profile;

  constructor(
    private readonly profileService: ProfileService,
    private readonly authenticationService: AuthenticationService,
    private readonly authStatusService: AuthenticationStatusService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly toastr: ToastrService,
    private readonly dialog: MatDialog,
    private readonly location: Location
  ) {}

  onDeleteUser(): void {
    this.dialog
      .open(ConfirmDialogComponent, {
        data: {
          message: `Delete the user?`,
          rejectText: 'Cancel',
          acceptText: 'Delete',
        } as ConfirmDialogParams,
        width: '328px',
        panelClass: 'mat-dialog-confirm',
      })
      .afterClosed()
      .pipe(
        untilDestroyed(this),
        filter((res) => Boolean(res)),
        switchMap(() => this.authenticationService.deleteUser$(this.userName))
      )
      .subscribe(() => {
        this.toastr.success('User successfully deleted.');
        this.authStatusService.logout();
        this.router.navigate(['/']);
      });
  }

  onSaveProfileInfo(event: ProfileInfoParams) {
    const { firstName, lastName, titleImage, isTwoFactorEnabled } = event;
    this.profileService
      .updateProfile$({
        firstName,
        lastName,
        userName: this.userName,
        settings: { isTwoFactorEnabled },
      })
      .pipe(
        untilDestroyed(this),
        switchMap(({ userName }) => {
          this.userName = userName;
          this.location.replaceState(`/profile/${userName}`);
          return !this.titleImageChanged
            ? of({})
            : this.profileService.updateTitleImage$(titleImage, this.userName);
        })
      )
      .subscribe(() => this.toastr.success('Profile info successfully saved.'));
  }

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        untilDestroyed(this),
        switchMap((paramMap) => {
          this.userName = paramMap.get('userName') || '';
          // this.isCurrentUser = userName === localStorage.getItem('userName');
          return this.profileService.getProfile$(this.userName as string);
        })
      )
      .subscribe((profile) => {
        this.profile = profile;
      });
  }
}
