import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedUiModule } from '@ldst/shared/ui';
import { ImageSliderComponent } from './image-slider.component';

@NgModule({
  imports: [CommonModule, SharedUiModule],
  exports: [ImageSliderComponent],
  declarations: [ImageSliderComponent],
})
export class ImageSliderModule {}
