import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  SimpleChanges,
  Output,
  ChangeDetectorRef,
  OnInit,
} from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ImageInfo, ImageValidators, ValidationConstants } from '@ldst/shared';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ProfileInfoParams } from '../../models/profile-info-params';
import { Profile } from '../../models/profile.model';

@UntilDestroy()
@Component({
  selector: 'ldst-profile-info-tab',
  templateUrl: './profile-info-tab.component.html',
  styleUrls: ['./profile-info-tab.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileInfoTabComponent implements OnInit, OnChanges {
  titleImage: ImageInfo = new ImageInfo();

  readonly firstNameTextLimit = 50;
  readonly lastNameTextLimit = 50;

  readonly validationConstants = ValidationConstants;

  form!: FormGroup;

  @Input() profile!: Profile;
  @Output() profileInfoSaved = new EventEmitter<ProfileInfoParams>();
  @Output() titleImageChanged = new EventEmitter<void>();

  get titleImageControl(): FormControl {
    return this.form.get('titleImage') as FormControl;
  }

  get firstNameControl(): AbstractControl {
    return this.form.get('firstName') as AbstractControl;
  }

  get isTwoFactorEnabledControl(): AbstractControl {
    return this.form.get('isTwoFactorEnabled') as AbstractControl;
  }

  get lastNameControl(): AbstractControl {
    return this.form.get('lastName') as AbstractControl;
  }

  constructor(
    private readonly imageValidators: ImageValidators,
    readonly changeDetectorRef: ChangeDetectorRef
  ) {
    this.form = new FormGroup({
      titleImage: new FormControl('', {
        validators: [
          this.imageValidators.checkSize(),
          this.imageValidators.checkFormat(),
        ],
      }),
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.firstNameTextLimit),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(this.lastNameTextLimit),
      ]),
      isTwoFactorEnabled: new FormControl(false),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['profile'] && !changes['profile'].firstChange) {
      const profile = changes['profile'].currentValue;
      this.form.patchValue(
        {
          firstName: profile.firstName,
          lastName: profile.lastName,
          isTwoFactorEnabled: profile.settings.isTwoFactorEnabled,
        },
        {
          emitEvent: false,
        }
      );
      this.titleImage.fileUrl = profile.titlePhotoPath;
      this.changeDetectorRef.markForCheck();
    }
  }

  ngOnInit(): void {
    this.titleImageControl.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.titleImageChanged.emit();
      });
  }

  onSave(): void {
    this.profileInfoSaved.emit(this.form.value as ProfileInfoParams);
    this.form.markAsPristine();
  }

  onDeleteTitleImage(): void {
    this.titleImage = new ImageInfo();
    this.titleImageControl.setValue(null);
  }
}
