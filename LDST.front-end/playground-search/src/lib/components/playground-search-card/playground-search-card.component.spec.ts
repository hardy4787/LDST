import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundSearchCardComponent } from './playground-search-card.component';

describe('PlaygroundSearchCardComponent', () => {
  let component: PlaygroundSearchCardComponent;
  let fixture: ComponentFixture<PlaygroundSearchCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlaygroundSearchCardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PlaygroundSearchCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
