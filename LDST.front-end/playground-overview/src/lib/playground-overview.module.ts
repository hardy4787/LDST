import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundOverviewPageComponent } from './playground-overview-page.component';
import { RouterModule } from '@angular/router';
import { PlaygroundOverviewService } from './services/playground-overview.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { GalleryComponent } from './gallery/gallery.component';
import { DaySchedulePipe } from './pipes/day-schedule.pipe';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundOverviewPageComponent,
      },
    ]),
    MatIconModule,
    MatButtonModule,
  ],
  providers: [PlaygroundOverviewService],
  declarations: [
    PlaygroundOverviewPageComponent,
    GalleryComponent,
    DaySchedulePipe,
  ],
})
export class PlaygroundOverviewModule {}
