import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CitySport } from '../../models/city-sport.model';
import { City } from '../../models/city.model';
import { SearchPlaygroundParams } from '../../models/search-playground-params.model';
import { Sport } from '../../models/sport.model';

@Component({
  selector: 'ldst-playground-search-card',
  templateUrl: './playground-search-card.component.html',
  styleUrls: ['./playground-search-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaygroundSearchCardComponent implements OnInit, OnChanges {
  @Input() citySports: CitySport[] = [];

  @Output() searchParamsChanged = new EventEmitter<SearchPlaygroundParams>();

  cities: City[] = [];
  sports: Sport[] = [];

  form!: FormGroup;

  get cityControl(): FormControl {
    return this.form.get('city') as FormControl;
  }

  get sportControl(): FormControl {
    return this.form.get('sport') as FormControl;
  }

  constructor() {
    this.form = new FormGroup({
      city: new FormControl('', Validators.required),
      sport: new FormControl(
        { value: '', disabled: true },
        Validators.required
      ),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    const citySports = changes['citySports'].currentValue as CitySport[];
    if (citySports) {
      this.cities = citySports.map(
        (x) => ({ cityId: x.cityId, cityName: x.cityName } as City)
      );
    }
  }

  ngOnInit(): void {
    this.cityControl.valueChanges.subscribe((cityId: number) => {
      this.sportControl.setValue('');
      this.sports =
        this.citySports.find((cs) => cs.cityId === cityId)?.sports ?? [];
      this.sports.length
        ? this.sportControl.enable()
        : this.sportControl.disable();
    });
  }

  onSubmit(): void {
    const { city: cityId, sport: sportId } = this.form.value;
    this.searchParamsChanged.emit({ cityId, sportId });
  }
}
