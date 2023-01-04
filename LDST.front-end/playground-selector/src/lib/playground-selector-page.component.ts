import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { PlaygroundView } from './models/playground-view.model';
import { PlaygroundSelectorService } from './services/playground-selector.service';

@Component({
  selector: 'ldst-playground-selector-page',
  templateUrl: './playground-selector-page.component.html',
  styleUrls: ['./playground-selector-page.component.scss'],
})
export class PlaygroundSelectorPageComponent implements OnInit {
  playgroundsViews!: PlaygroundView[];

  private cityId!: number;
  private sportId!: number;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly playgroundSelectorService: PlaygroundSelectorService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        switchMap(({ cityId, sportId }) => {
          this.cityId = cityId;
          this.sportId = sportId;
          return this.playgroundSelectorService.getPlayground$(cityId, sportId);
        })
      )
      .subscribe((response) => {
        this.playgroundsViews = response;
      });
  }

  onNavigateToPlaygroundOverview(playgroundId: number): void {
    this.router.navigate(['/playground-overview'], {
      queryParams: { playgroundId, cityId: this.cityId, sportId: this.sportId },
    });
  }
}
