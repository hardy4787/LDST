import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundSelectorPageComponent } from './playground-selector-page.component';
import { RouterModule } from '@angular/router';
import { PlaygroundSelectorService } from './services/playground-selector.service';
import { ImageSliderModule } from '@ldst/organisms';
import { PlaygroundsDayComponent } from './components/playgrounds-day/playgrounds-day.component';
import { PlaygroundSlotsComponent } from './components/playground-slots/playground-slots.component';
import { SharedModule } from '@ldst/shared';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundSelectorPageComponent,
      },
    ]),
    ImageSliderModule,
  ],
  providers: [PlaygroundSelectorService],
  declarations: [
    PlaygroundSelectorPageComponent,
    PlaygroundsDayComponent,
    PlaygroundSlotsComponent,
  ],
})
export class PlaygroundSelectorModule {}
