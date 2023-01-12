import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePlaygroundPageComponent } from './create-playground-page.component';
import { CreatePlaygroundService } from './services/create-playground.service';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@ldst/shared';
import { CreatePlaygroundStepComponent } from './components/create-playground-step/create-playground-step.component';
import { UploadImagesStepComponent } from './components/upload-images-step/upload-images-step.component';
import { CreateTimeSlotsStepComponent } from './components/create-time-slots-step/create-time-slots-step.component';
import { DragAndDropDirective } from './directives/drag-and-drop.directive';
import { PlaygroundStore } from './services/playground.store';
import { ImageValidators } from './services/image.validators';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { ImageCardComponent } from './components/image-card/image-card.component';
import { DayScheduleComponent } from './components/day-schedule/day-schedule.component';

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
  ],
  declarations: [
    CreatePlaygroundPageComponent,
    CreatePlaygroundStepComponent,
    UploadImagesStepComponent,
    DragAndDropDirective,
    CreateTimeSlotsStepComponent,
    ImageCardComponent,
    DayScheduleComponent,
  ],
  providers: [CreatePlaygroundService, ImageValidators, PlaygroundStore],
})
export class CreatePlaygroundModule {}
