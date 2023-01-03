import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadPhotoStepComponent } from './upload-photo-step.component';

describe('UploadPhotoStepComponent', () => {
  let component: UploadPhotoStepComponent;
  let fixture: ComponentFixture<UploadPhotoStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UploadPhotoStepComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UploadPhotoStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
