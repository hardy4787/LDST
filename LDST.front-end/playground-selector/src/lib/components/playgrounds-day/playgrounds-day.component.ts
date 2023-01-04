import { ChangeDetectionStrategy, EventEmitter, Output } from '@angular/core';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Playground } from '../../models/playground.model';

@Component({
  selector: 'ldst-playgrounds-day',
  templateUrl: './playgrounds-day.component.html',
  styleUrls: ['./playgrounds-day.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaygroundsDayComponent {
  @Input() playgrounds!: Playground[];
  @Output() playgroundOverviewSelected = new EventEmitter<number>();

  constructor() {}

  onSelectPlaygroundOverview(playgroundId: number): void {
    this.playgroundOverviewSelected.emit(playgroundId);
  }

  playgroundsTrackBy = (_: number, details: Playground): number => details.id;
}
