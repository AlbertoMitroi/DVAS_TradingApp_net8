import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing'; 
import { CarouselOfSymbolsComponent } from './carousel-of-symbols.component';
import { CarouselModule } from 'primeng/carousel';
describe('CarouselOfSymbolsComponent', () => {
  let component: CarouselOfSymbolsComponent;
  let fixture: ComponentFixture<CarouselOfSymbolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CarouselOfSymbolsComponent],
      imports: [HttpClientTestingModule,
        CarouselModule
      ]
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
