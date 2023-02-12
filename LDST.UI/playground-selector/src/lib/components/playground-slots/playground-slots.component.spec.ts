import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundSlotsComponent } from './playground-slots.component';

describe('PlaygroundSlotsComponent', () => {
  let component: PlaygroundSlotsComponent;
  let fixture: ComponentFixture<PlaygroundSlotsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundSlotsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundSlotsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
