import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DayScheduleComponent } from './day-schedule.component';

describe('DayScheduleComponent', () => {
  let component: DayScheduleComponent;
  let fixture: ComponentFixture<DayScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DayScheduleComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DayScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
