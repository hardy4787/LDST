import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundsDayComponent } from './playgrounds-day.component';

describe('PlaygroundsDayComponent', () => {
  let component: PlaygroundsDayComponent;
  let fixture: ComponentFixture<PlaygroundsDayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundsDayComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundsDayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
