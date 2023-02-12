import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundSelectorPageComponent } from './playground-selector-page.component';

describe('PlaygroundSelectorPageComponent', () => {
  let component: PlaygroundSelectorPageComponent;
  let fixture: ComponentFixture<PlaygroundSelectorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundSelectorPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundSelectorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
