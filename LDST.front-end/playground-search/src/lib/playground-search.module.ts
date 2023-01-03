import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaygroundSearchPageComponent } from './playground-search-page.component';
import { SharedUiModule } from '@ldst/shared/ui';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { PlaygroundSearchCardComponent } from './components/playground-search-card/playground-search-card.component';
import { PlaygroundSearchService } from './services/playground-search.service';
import { HttpClientModule } from '@angular/common/http';
import { ImageSliderModule } from '@ldst/organisms';

@NgModule({
  imports: [
    CommonModule,
    SharedUiModule,
    HttpClientModule,
    RouterModule.forChild([
      {
        path: '',
        component: PlaygroundSearchPageComponent,
      },
    ]),
    MatIconModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    ImageSliderModule,
  ],
  providers: [PlaygroundSearchService],
  declarations: [PlaygroundSearchPageComponent, PlaygroundSearchCardComponent],
})
export class PlaygroundSearchModule {}
