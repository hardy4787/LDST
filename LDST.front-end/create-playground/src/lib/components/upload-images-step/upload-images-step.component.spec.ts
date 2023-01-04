import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadImagesStepComponent } from './upload-images-step.component';

describe('UploadPhotoStepComponent', () => {
  let component: UploadImagesStepComponent;
  let fixture: ComponentFixture<UploadImagesStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UploadImagesStepComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UploadImagesStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
