import { Component, HostListener } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Chart } from 'chart.js';

import {
  HistoryEntry,
  CompanyDetails,
  CompanyObject,
} from '../market-table/market-table.component';

import { SharedCompanyService } from '../../services/shared-company/shared-company.service';

interface Company {
  companySymbol: string;
  company: {
    name: string;
    symbol: string;
  };
  volume: number;
  price: number;
}

@Component({
  selector: 'app-top-xtable',
  templateUrl: './top-xtable.component.html',
  styleUrl: './top-xtable.component.css',
})
export class TopXTableComponent {
  isMobileView: boolean = window.innerWidth < 768;
  public companies: CompanyObject[] = [];
  public selectedCompany: CompanyObject | null = null;
  private chart: Chart | null = null;

  constructor(
    private http: HttpClient,
    private sharedCompanyService: SharedCompanyService
  ) {}

  ngOnInit() {
    this.getCompanies();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isMobileView = window.innerWidth < 768;
    if (this.chart) {
      this.chart.resize();
    }
  }

  getCompanies() {
    let params = new HttpParams();

    params = params.set('orderToggle', 'desc');
    this.http
      .get<CompanyObject[]>(
        'https://localhost:7221/api/CompanyInventory/topXCompaniesByParameter',
        { params } 
      )
      .subscribe(
        (result) => {
          this.companies = result;
          if (this.companies.length > 0) {
            this.selectedCompany = this.companies[0];
          }
        },
        (error) => {
          console.log(error);
        }
      );
  }

  onRowSelect(company: any) {
    this.selectedCompany = company;
    this.sharedCompanyService.setValue(company);
  }

 

}
