import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { PlaygroundOverview } from './models/playground-overview.model';
import { PlaygroundOverviewService } from './services/playground-overview.service';

@Component({
  selector: 'ldst-playground-overview-page',
  templateUrl: './playground-overview-page.component.html',
  styleUrls: ['./playground-overview-page.component.scss'],
})
export class PlaygroundOverviewPageComponent implements OnInit {
  playground!: PlaygroundOverview;

  private cityId!: number;
  private sportId!: number;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly playgroundOverviewService: PlaygroundOverviewService
  ) {}

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        switchMap(({ playgroundId, cityId, sportId }) => {
          this.cityId = cityId;
          this.sportId = sportId;
          return this.playgroundOverviewService.getPlaygroundOverview$(
            playgroundId
          );
        })
      )
      .subscribe((response) => {
        this.playground = response;
      });
  }

  onNavigateToMainPage(): void {
    this.router.navigate(['/playground-search']);
  }

  onNavigateToPlaygroundsListPage(): void {
    this.router.navigate(['/playground-selector'], {
      queryParams: { cityId: this.cityId, sportId: this.sportId },
    });
  }
}
