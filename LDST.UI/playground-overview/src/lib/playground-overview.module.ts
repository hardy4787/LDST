import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundOverviewPageComponent } from './playground-overview-page.component';
import { RouterModule } from '@angular/router';
import { PlaygroundOverviewService } from './services/playground-overview.service';
import { GalleryComponent } from './gallery/gallery.component';
import { DaySchedulePipe } from './pipes/day-schedule.pipe';
import { SharedModule } from '@ldst/shared';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundOverviewPageComponent,
      },
    ]),
  ],
  providers: [PlaygroundOverviewService],
  declarations: [
    PlaygroundOverviewPageComponent,
    GalleryComponent,
    DaySchedulePipe,
  ],
})
export class PlaygroundOverviewModule {}
