import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageInfo, ValidationConstants } from '@ldst/shared';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ImageValidators } from '@ldst/shared';
import { concatMap, debounceTime, of, Subject, switchMap } from 'rxjs';
import { ProfileService } from './services/profile.service';
import { UpdateProfileParams } from './models/update-profile-params.model';

@UntilDestroy()
@Component({
  selector: 'ldst-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss'],
})
export class ProfilePageComponent implements OnInit {
  isCurrentUser = false;
  titleImage: ImageInfo = new ImageInfo();
  titleImageControl!: FormControl;
  savingStatus = '';
  userName!: string;

  readonly firstNameTextLimit = 50;
  readonly lastNameTextLimit = 50;

  readonly validationConstants = ValidationConstants;

  form!: FormGroup;

  get firstNameControl(): AbstractControl {
    return this.form.get('firstName') as AbstractControl;
  }

  get lastNameControl(): AbstractControl {
    return this.form.get('lastName') as AbstractControl;
  }

  constructor(
    private readonly profileService: ProfileService,
    private readonly imageValidators: ImageValidators,
    private readonly route: ActivatedRoute
  ) {
    this.titleImageControl = new FormControl('', {
      validators: [
        this.imageValidators.checkSize(),
        this.imageValidators.checkFormat(),
        Validators.required,
      ],
    });

    this.form = new FormGroup({
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.firstNameTextLimit),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.lastNameTextLimit),
      ]),
    });
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
        this.firstNameControl.setValue(profile.firstName, { emitEvent: false });
        this.lastNameControl.setValue(profile.lastName, { emitEvent: false });
        this.titleImage.fileUrl = profile.titlePhotoPath;
      });

    this.form.valueChanges
      .pipe(untilDestroyed(this), debounceTime(500))
      .pipe(
        concatMap((value) => {
          this.savingStatus = 'Saving changes...';
          return this.profileService.updateProfile$({
            ...value,
            userName: this.userName,
          });
        })
      )
      .subscribe(() => {
        this.savingStatus = 'Saved.';
      });

    this.titleImageControl.valueChanges
      .pipe(untilDestroyed(this))
      .pipe(
        concatMap((value) => {
          return this.profileService.updateTitleImage$(value, this.userName);
        })
      )
      .subscribe();
  }

  onDeleteTitleImage(): void {
    this.titleImage = new ImageInfo();
    this.titleImageControl.setValue(null);
  }
}
