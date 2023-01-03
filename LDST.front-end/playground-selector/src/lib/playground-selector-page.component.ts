import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { PlaygroundView } from './models/playground-view.model';
import { Playground } from './models/playground.model';
import { PlaygroundSelectorService } from './services/playground-selector.service';

@Component({
  selector: 'ldst-playground-selector-page',
  templateUrl: './playground-selector-page.component.html',
  styleUrls: ['./playground-selector-page.component.scss'],
})
export class PlaygroundSelectorPageComponent implements OnInit {
  playgroundsViews!: PlaygroundView[];

  constructor(
    private readonly route: ActivatedRoute,
    private readonly playgroundSelectorService: PlaygroundSelectorService
  ) {}

  ngOnInit(): void {
    this.route.queryParams
      .pipe(
        switchMap((queryParams) => {
          const { cityId, sportId } = queryParams;
          return this.playgroundSelectorService.getPlayground$(cityId, sportId);
        })
      )
      .subscribe((response) => {
        this.playgroundsViews = response;
      });
  }
}
