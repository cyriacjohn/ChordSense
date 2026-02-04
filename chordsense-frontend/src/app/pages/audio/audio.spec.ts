import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudioComponent } from './audio';

describe('Audio', () => {
  let component: AudioComponent;
  let fixture: ComponentFixture<AudioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AudioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AudioComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
