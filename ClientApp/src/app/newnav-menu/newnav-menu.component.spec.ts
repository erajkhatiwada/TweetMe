import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewnavMenuComponent } from './newnav-menu.component';

describe('NewnavMenuComponent', () => {
  let component: NewnavMenuComponent;
  let fixture: ComponentFixture<NewnavMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewnavMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewnavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
