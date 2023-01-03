import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTimeSlotsStepComponent } from './create-time-slots-step.component';

describe('CreateTimeSlotsStepComponent', () => {
  let component: CreateTimeSlotsStepComponent;
  let fixture: ComponentFixture<CreateTimeSlotsStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateTimeSlotsStepComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateTimeSlotsStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
