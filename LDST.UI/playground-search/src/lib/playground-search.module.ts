import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundSearchPageComponent } from './playground-search-page.component';
import { SharedModule } from '@ldst/shared';
import { RouterModule } from '@angular/router';
import { PlaygroundSearchCardComponent } from './components/playground-search-card/playground-search-card.component';
import { PlaygroundSearchService } from './services/playground-search.service';
import { HttpClientModule } from '@angular/common/http';
import { ImageSliderModule } from '@ldst/organisms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundSearchPageComponent,
      },
    ]),
    ImageSliderModule,
  ],
  providers: [PlaygroundSearchService],
  declarations: [PlaygroundSearchPageComponent, PlaygroundSearchCardComponent],
})
export class PlaygroundSearchModule {}
