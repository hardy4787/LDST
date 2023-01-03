import { ChangeDetectionStrategy, Input } from '@angular/core';
import { Component } from '@angular/core';
import { Playground } from '../../models/playground.model';

@Component({
  selector: 'ldst-playground-slots',
  templateUrl: './playground-slots.component.html',
  styleUrls: ['./playground-slots.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaygroundSlotsComponent {
  @Input() playground!: Playground;
}
