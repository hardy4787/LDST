import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { CitySport } from './models/city-sport.model';
import { SearchPlaygroundParams } from './models/search-playground-params.model';
import { PlaygroundSearchService } from './services/playground-search.service';

@UntilDestroy()
@Component({
  templateUrl: 'playground-search-page.component.html',
  styleUrls: ['./playground-search-page.component.scss'],
})
export class PlaygroundSearchPageComponent implements OnInit {
  citySports: CitySport[] = [];

  constructor(
    private readonly router: Router,
    private readonly playgroundSearchService: PlaygroundSearchService
  ) {}

  ngOnInit(): void {
    this.playgroundSearchService
      .getCitySports$()
      .pipe(untilDestroyed(this))
      .subscribe((citySports) => (this.citySports = citySports));
  }

  onNavigateToPlaygrounds({ cityId, sportId }: SearchPlaygroundParams): void {
    this.router.navigate(['/playground-selector'], {
      queryParams: { cityId, sportId },
    });
  }
}
