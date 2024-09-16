import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MarketSearchBarComponent } from './market-search-bar.component';

describe('MarketsComponent', () => {
  let component: MarketSearchBarComponent;
  let fixture: ComponentFixture<MarketSearchBarComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MarketSearchBarComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketSearchBarComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve company data from the server', () => {
    const mockCompanyData = { symbol: 'AAPL', name: 'Apple Inc.' };

    component.value = 'AAPL';

    const req = httpMock.expectOne('/api/companies/AAPL');
    expect(req.request.method).toEqual('GET');
    req.flush(mockCompanyData);

    //expect(component.companyData).toEqual(mockCompanyData);
  });
});
