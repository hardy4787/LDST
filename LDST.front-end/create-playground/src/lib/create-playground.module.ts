import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePlaygroundPageComponent } from './create-playground-page.component';
import { CreatePlaygroundService } from './services/create-playground.service';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { SharedModule } from '@ldst/shared';
import { CreatePlaygroundStepComponent } from './components/create-playground-step/create-playground-step.component';
import { UploadPhotoStepComponent } from './components/upload-photo-step/upload-photo-step.component';
import { CreateTimeSlotsStepComponent } from './components/create-time-slots-step/create-time-slots-step.component';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DragAndDropDirective } from './directives/drag-and-drop.directive';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { TimeSlotsValidators } from './services/time-slots.validators';
import { PlaygroundStore } from './services/playground.store';
import { ImageValidators } from './services/image.validators';
import {
  MatSnackBarModule,
  MAT_SNACK_BAR_DEFAULT_OPTIONS,
} from '@angular/material/snack-bar';
import { PictureCardComponent } from './components/picture-card/picture-card.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: CreatePlaygroundPageComponent,
      },
    ]),
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatIconModule,
    MatStepperModule,
    MatProgressBarModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatButtonToggleModule,
    MatSnackBarModule,
  ],
  declarations: [
    CreatePlaygroundPageComponent,
    CreatePlaygroundStepComponent,
    UploadPhotoStepComponent,
    DragAndDropDirective,
    CreateTimeSlotsStepComponent,
    PictureCardComponent,
  ],
  providers: [
    CreatePlaygroundService,
    TimeSlotsValidators,
    ImageValidators,
    PlaygroundStore,
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: {
        duration: 5000,
        panelClass: ['gray-snackbar'],
      },
    },
  ],
})
export class CreatePlaygroundModule {}
