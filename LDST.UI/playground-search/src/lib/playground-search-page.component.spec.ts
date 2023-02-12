import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundSearchPageComponent } from './playground-search-page.component';

describe('PlaygroundSearchPageComponent', () => {
  let component: PlaygroundSearchPageComponent;
  let fixture: ComponentFixture<PlaygroundSearchPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundSearchPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundSearchPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
