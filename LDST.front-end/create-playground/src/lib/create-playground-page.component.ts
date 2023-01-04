import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { filter, forkJoin, map, switchMap, take } from 'rxjs';
import { City } from './models/city.model';
import { Sport } from './models/sport.model';
import { CreatePlaygroundService } from './services/create-playground.service';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { PlaygroundStore } from './services/playground.store';
import { TimeSlotsValidators } from './services/time-slots.validators';
import { ImageValidators } from './services/image.validators';
import { MatStepper } from '@angular/material/stepper';
import { FormControlUtils } from '@ldst/utils';
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
  get playgroundImagesControl(): FormArray {
    return this.form.controls['playgroundImages'] as FormArray;
  }
  get timeSlotsConfigurationControl(): FormGroup {
    return this.form.controls['timeSlotsConfiguration'] as FormGroup;
  }

  constructor(
    private readonly createPlaygroundService: CreatePlaygroundService,
    private readonly playgroundStore: PlaygroundStore,
    private readonly timeSlotsValidators: TimeSlotsValidators,
    private readonly imageValidators: ImageValidators,
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
          this.imageValidators.checkSize(),
          this.imageValidators.checkFormat(),
          Validators.required,
        ],
      }),
      playgroundImages: new FormArray([]),
      timeSlotsConfiguration: new FormGroup({
        price: new FormControl('', Validators.required),
        gameTime: new FormControl('', Validators.required),
        daysCount: new FormControl('', Validators.required),
        openPlaygroundTime: new FormControl('', Validators.required),
        closePlaygroundTime: new FormControl('', {
          validators: [Validators.required],
        }),
      }),
    });

    this.timeSlotsConfigurationControl.addValidators(
      this.timeSlotsValidators.checkTimeInterval(
        this.timeSlotsConfigurationControl.controls['openPlaygroundTime'],
        this.timeSlotsConfigurationControl.controls['closePlaygroundTime'],
        this.timeSlotsConfigurationControl.controls['gameTime']
      )
    );
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
      this.playgroundStore.galleryImages$.pipe(
        filter((galleryImages) => Boolean(galleryImages)),
        map((galleryImages) => galleryImages.map((i) => i.file as File)),
        take(1)
      ),
      this.playgroundStore.timeSlots$.pipe(
        filter((timeSlots) => Boolean(timeSlots)),
        take(1)
      ),
    ])
      .pipe(
        untilDestroyed(this),
        switchMap(([playgroundInfo, titleImage, galleryImages, timeSlots]) => {
          return this.createPlaygroundService
            .createPlayground$(hostId, playgroundInfo)
            .pipe(
              switchMap((playgroundId) => {
                const requests = [];
                requests.push(
                  this.createPlaygroundService.createTimeSlots$(
                    playgroundId,
                    timeSlots
                  )
                );
                if (titleImage) {
                  requests.push(
                    this.createPlaygroundService.uploadTitleImage$(
                      playgroundId,
                      titleImage
                    )
                  );
                }
                if (galleryImages.length) {
                  requests.push(
                    this.createPlaygroundService.uploadGalleryImages$(
                      playgroundId,
                      galleryImages
                    )
                  );
                }
                return forkJoin(requests);
              })
            );
        })
      )
      .subscribe(() => {
        this.snackBar.open('Playground saved.');
      });
  }
}
