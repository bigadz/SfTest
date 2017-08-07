import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoPlayComponent } from './auto-play.component';

describe('AutoPlayComponent', () => {
  let component: AutoPlayComponent;
  let fixture: ComponentFixture<AutoPlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoPlayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
