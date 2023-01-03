import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { filter, forkJoin, switchMap, take } from 'rxjs';
import { City } from './models/city.model';
import { Sport } from './models/sport.model';
import { CreatePlaygroundService } from './services/create-playground.service';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { PlaygroundStore } from './services/playground.store';
import { TimeSlotsValidators } from './services/time-slots.validators';
import { TitleImageValidators } from './services/title-image.validators';
import { MatStepper } from '@angular/material/stepper';
import { FormControlUtils } from '@ldst/utils';
import { PlaygroundTitleImage } from './models/playground-title-image.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@UntilDestroy()
@Component({
  templateUrl: './create-playground-page.component.html',
  styleUrls: ['./create-playground-page.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false },
    },
  ],
})
export class CreatePlaygroundPageComponent implements OnInit {
  cities = [] as City[];
  sports = [] as Sport[];
  form!: FormGroup;

  isTimeSlotsGenerated$ = this.playgroundStore.isTimeSlotsGenerated$;

  get createPlaygroundControl(): FormGroup {
    return this.form.controls['createPlayground'] as FormGroup;
  }
  get titleImageControl(): FormControl {
    return this.form.controls['titleImage'] as FormControl;
  }
  get timeSlotsConfigurationControl(): FormGroup {
    return this.form.controls['timeSlotsConfiguration'] as FormGroup;
  }

  constructor(
    private readonly createPlaygroundService: CreatePlaygroundService,
    private readonly playgroundStore: PlaygroundStore,
    private readonly timeSlotsValidators: TimeSlotsValidators,
    private readonly titleImageValidators: TitleImageValidators,
    private readonly snackBar: MatSnackBar
  ) {
    this.form = new FormGroup({
      createPlayground: new FormGroup({
        name: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        sportId: new FormControl('', Validators.required),
        address1: new FormControl('', Validators.required),
        address2: new FormControl(''),
        cityId: new FormControl('', Validators.required),
        state: new FormControl(''),
        zipCode: new FormControl('', Validators.required),
      }),
      titleImage: new FormControl('', {
        validators: [
          this.titleImageValidators.checkSize(),
          this.titleImageValidators.checkFormat(),
        ],
      }),
      timeSlotsConfiguration: new FormGroup({
        price: new FormControl('', Validators.required),
        gameTime: new FormControl('', Validators.required),
        daysCount: new FormControl('', Validators.required),
        openPlaygroundTime: new FormControl('', Validators.required),
        closePlaygroundTime: new FormControl('', {
          validators: [
            Validators.required,
            this.timeSlotsValidators.checkTimeSlotsGap(),
          ],
        }),
      }),
    });
  }

  ngOnInit(): void {
    forkJoin([
      this.createPlaygroundService.getCities$(),
      this.createPlaygroundService.getSports$(),
    ])
      .pipe(untilDestroyed(this))
      .subscribe(([cities, sports]) => {
        this.cities = cities;
        this.sports = sports;
      });
  }

  goForward(stepper: MatStepper, control: FormGroup | FormControl) {
    if (control.invalid) {
      FormControlUtils.markAllAsTouched(control);
      return;
    }
    stepper.next();
  }

  onSavePlayground(): void {
    const hostId = '6a80dea6-3c6c-4aa2-b072-060495b04718';
    forkJoin([
      this.playgroundStore.playgroundInfo$.pipe(
        filter((playgroundInfo) => Boolean(playgroundInfo)),
        take(1)
      ),
      this.playgroundStore.titleImage$.pipe(
        filter((titleImage) => Boolean(titleImage)),
        take(1)
      ),
      this.playgroundStore.timeSlots$.pipe(
        filter((timeSlots) => Boolean(timeSlots)),
        take(1)
      ),
    ])
      .pipe(
        untilDestroyed(this),
        switchMap(([playgroundInfo, titleImage, timeSlots]) => {
          return this.createPlaygroundService
            .createPlayground$(hostId, playgroundInfo)
            .pipe(
              switchMap((playgroundId) => {
                const uploadFile = {
                  playgroundId,
                  titleImage: titleImage,
                } as PlaygroundTitleImage;
                return forkJoin([
                  this.createPlaygroundService.uploadTitleImage$(uploadFile),
                  this.createPlaygroundService.createTimeSlots$(
                    playgroundId,
                    timeSlots
                  ),
                ]);
              })
            );
        })
      )
      .subscribe(() => {
        this.snackBar.open('Playground saved.');
      });
  }
}
