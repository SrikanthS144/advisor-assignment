import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAdviseComponent } from './add-advise.component';

describe('AddAdviseComponent', () => {
  let component: AddAdviseComponent;
  let fixture: ComponentFixture<AddAdviseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAdviseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAdviseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
