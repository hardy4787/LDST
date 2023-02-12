import { OnInit } from '@angular/core';
import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidationConstants } from '@ldst/shared';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { City } from '../../models/city.model';
import { Sport } from '../../models/sport.model';
import { PlaygroundStore } from '../../services/playground.store';

@UntilDestroy()
@Component({
  selector: 'ldst-create-playground-step',
  templateUrl: './create-playground-step.component.html',
  styleUrls: ['./create-playground-step.component.scss'],
})
export class CreatePlaygroundStepComponent implements OnInit {
  readonly validationConstants = ValidationConstants;
  @Input() sports = [] as Sport[];
  @Input() cities = [] as City[];
  @Input() form!: FormGroup;

  constructor(private readonly playgroundStore: PlaygroundStore) {}

  ngOnInit(): void {
    this.form.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((value) => this.playgroundStore.updatePlaygroundInfo(value));
  }
}
