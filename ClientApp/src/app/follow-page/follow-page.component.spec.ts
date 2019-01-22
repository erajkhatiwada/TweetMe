import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowPageComponent } from './follow-page.component';

describe('FollowPageComponent', () => {
  let component: FollowPageComponent;
  let fixture: ComponentFixture<FollowPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FollowPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FollowPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
