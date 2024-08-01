import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeaterForecastIndexComponent } from './weater-forecast-index.component';

describe('WeaterForecastIndexComponent', () => {
  let component: WeaterForecastIndexComponent;
  let fixture: ComponentFixture<WeaterForecastIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeaterForecastIndexComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeaterForecastIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
