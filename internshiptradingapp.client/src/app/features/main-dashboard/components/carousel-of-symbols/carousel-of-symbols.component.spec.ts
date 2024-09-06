import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarouselOfSymbolsComponent } from './carousel-of-symbols.component';

describe('CarouselOfSymbolsComponent', () => {
  let component: CarouselOfSymbolsComponent;
  let fixture: ComponentFixture<CarouselOfSymbolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CarouselOfSymbolsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarouselOfSymbolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
