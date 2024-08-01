import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainDashboardIndexComponent } from './main-dashboard-index.component';

describe('MainDashboardIndexComponent', () => {
  let component: MainDashboardIndexComponent;
  let fixture: ComponentFixture<MainDashboardIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainDashboardIndexComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainDashboardIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
