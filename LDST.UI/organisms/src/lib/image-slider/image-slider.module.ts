import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@ldst/shared';
import { ImageSliderComponent } from './image-slider.component';

@NgModule({
  imports: [CommonModule, SharedModule],
  exports: [ImageSliderComponent],
  declarations: [ImageSliderComponent],
})
export class ImageSliderModule {}
