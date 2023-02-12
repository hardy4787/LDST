import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlaygroundPageComponent } from './create-playground-page.component';

describe('CreatePlaygroundPageComponent', () => {
  let component: CreatePlaygroundPageComponent;
  let fixture: ComponentFixture<CreatePlaygroundPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatePlaygroundPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CreatePlaygroundPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
