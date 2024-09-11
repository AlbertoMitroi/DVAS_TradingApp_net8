import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-market-search-bar',
  templateUrl: './market-search-bar.component.html',
  styleUrls: ['./market-search-bar.component.css'],
})
export class MarketSearchBarComponent implements OnInit {
  public symbolFilter: string | null = null;
  public companyData: any = {};
  public latestHistoryEntry: any = null;

  public data: any;
  public options: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCompanyBySymbol();
  }

  getCompanyBySymbol() {
    if (this.symbolFilter && this.symbolFilter.trim()) {
      this.http
        .get(`https://localhost:7221/api/CompanyInventory/`, {
          params: {
            symbol: this.symbolFilter,
          },
        })
        .subscribe(
          (response: any) => {
            this.companyData = response;
            console.log('Company data:', this.companyData);

            if (
              this.companyData.history &&
              typeof this.companyData.history[Symbol.iterator] === 'function'
            ) {
              const historyArray = Array.from(this.companyData.history);
              this.latestHistoryEntry = historyArray[historyArray.length - 1];
              console.log('Latest history entry:', this.latestHistoryEntry);
            } else {
              console.log('History is not iterable or not present');
            }
          },
          (error) => {
            console.error('Error fetching company data:', error);
          }
        );
    } else {
      console.error(
        'symbolFilter is null, undefined, or empty. API call not made.'
      );
    }
  }
}
