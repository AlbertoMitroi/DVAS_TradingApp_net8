import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MarketsComponent } from './markets.component';

describe('MarketsComponent', () => {
  let component: MarketsComponent;
  let fixture: ComponentFixture<MarketsComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MarketsComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketsComponent);
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

    component.symbolFilter = 'AAPL';
    component.getCompanyBySymbol();

    const req = httpMock.expectOne('/api/companies/AAPL');
    expect(req.request.method).toEqual('GET');
    req.flush(mockCompanyData);

    expect(component.companyData).toEqual(mockCompanyData);
  });
});
