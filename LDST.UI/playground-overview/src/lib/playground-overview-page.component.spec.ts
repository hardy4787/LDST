import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundOverviewPageComponent } from './playground-overview-page.component';

describe('PlaygroundOverviewPageComponent', () => {
  let component: PlaygroundOverviewPageComponent;
  let fixture: ComponentFixture<PlaygroundOverviewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundOverviewPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundOverviewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
