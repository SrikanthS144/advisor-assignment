import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAdviseComponent } from './list-advise.component';

describe('ListAdviseComponent', () => {
  let component: ListAdviseComponent;
  let fixture: ComponentFixture<ListAdviseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListAdviseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListAdviseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
