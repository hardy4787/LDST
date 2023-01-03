import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundSelectorPageComponent } from './playground-selector-page.component';
import { RouterModule } from '@angular/router';
import { PlaygroundSelectorService } from './services/playground-selector.service';
import { ImageSliderModule } from '@ldst/organisms';
import { MatTabsModule } from '@angular/material/tabs';
import { PlaygroundsDayComponent } from './components/playgrounds-day/playgrounds-day.component';
import { PlaygroundSlotsComponent } from './components/playground-slots/playground-slots.component';
import { SharedUiModule } from '@ldst/shared/ui';

@NgModule({
  imports: [
    CommonModule,
    SharedUiModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundSelectorPageComponent,
      },
    ]),
    ImageSliderModule,
    MatTabsModule,
  ],
  providers: [PlaygroundSelectorService],
  declarations: [
    PlaygroundSelectorPageComponent,
    PlaygroundsDayComponent,
    PlaygroundSlotsComponent,
  ],
})
export class PlaygroundSelectorModule {}
