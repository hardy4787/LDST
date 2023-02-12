import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlaygroundStepComponent } from './create-playground-step.component';

describe('CreatePlaygroundStepComponent', () => {
  let component: CreatePlaygroundStepComponent;
  let fixture: ComponentFixture<CreatePlaygroundStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatePlaygroundStepComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreatePlaygroundStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
