import { Component, Input } from '@angular/core';
import { Playground } from '../../models/playground.model';

@Component({
  selector: 'ldst-playgrounds-day',
  templateUrl: './playgrounds-day.component.html',
  styleUrls: ['./playgrounds-day.component.scss'],
})
export class PlaygroundsDayComponent {
  @Input() playgrounds!: Playground[];
}
