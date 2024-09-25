import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-carousel-of-symbols',
  templateUrl: './carousel-of-symbols.component.html',
  styleUrls: ['./carousel-of-symbols.component.css']
})
export class CarouselOfSymbolsComponent implements OnInit {
  public companyData: any[] = [];
  public companyHistoryEntries: any[] = [];
  public symbolFilter: string | null = null;
  responsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 8,
      numScroll: 8
    },
    {
      breakpoint: '768px',
      numVisible: 8,
      numScroll: 8
    },
    {
      breakpoint: '560px',
      numVisible: 8,
      numScroll: 8
    },
    {
      breakpoint: '360px',
      numVisible: 12,
      numScroll: 12
    }
  ];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getCompanyDetails();
  }

  getCompanyDetails() {
    this.http.get<any[]>(`https://localhost:7221/api/CompanyInventory/`, {
      params: this.symbolFilter ? { symbol: this.symbolFilter } : {}
    })
      .subscribe(
        (response: any[]) => {
          this.companyData = response;
          //console.log('Company data:', this.companyData);

          this.companyHistoryEntries = this.companyData
            .filter(company => company.history && company.history.length > 0)
            .map(company => ({
              companySymbol: company.symbol,
              ...company.history[company.history.length - 1]
            }));

          //console.log('Latest history entries:', this.companyHistoryEntries);
        },
        (error) => {
          console.error('Error fetching company data:', error);
        }
      );
  }
}
