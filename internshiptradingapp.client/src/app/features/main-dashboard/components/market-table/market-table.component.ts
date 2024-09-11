import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, AfterViewInit, HostListener } from '@angular/core';
import { Chart } from 'chart.js';

// interface Company {
//   name: string;
//   symbol: string;
//   price: number;
//   referencePrice: number;
//   openingPrice: number;
//   closingPrice: number;
//   eps: number;
//   per: number;
//   dayVariation: number;
//   status: number;
// }

interface HistoryEntry {
  id: number;
  companySymbol: string;
  price: number;
  referencePrice: number;
  openingPrice: number;
  closingPrice: number;
  per: number;
  dayVariation: number;
  eps: number;
  date: string;
}

interface CompanyDetails {
  id: number;
  name: string;
  symbol: string;
  status: number;
  history: HistoryEntry[] | null;
}

interface CompanyObject {
  company: CompanyDetails;
  history: HistoryEntry[];
}

@Component({
  selector: 'app-market-table',
  templateUrl: './market-table.component.html',
  styleUrls: ['./market-table.component.css'],
})
export class MarketTableComponent {
  isMobileView: boolean = window.innerWidth < 768;
  public companies: CompanyObject[] = [];
  public expandedRows: { [key: number]: boolean } = {};
  // public companyToDisplay: Company[] = [];
  public selectedCompany: CompanyObject | null = null;
  // public splitCompanies: Company[][] = [];
  private chart: Chart | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCompanies();
    console.log(this.companies);
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isMobileView = window.innerWidth < 768;
  }

  getCompanies() {
    let params = new HttpParams();
    this.http
      .get<CompanyObject[]>(
        'https://localhost:7221/api/CompanyInventory/topXCompaniesByParameter'
      )
      .subscribe(
        (result) => {
          this.companies = result;

          console.log(this.companies);

          console.log('received response from server');

          if (this.companies.length > 0) {
            this.selectedCompany = this.companies[0];
            setTimeout(() => this.initializeChart(), 1);
          }
        },
        (error) => {
          console.log(error);
        }
      );
  }

  // getCompanyBySymbol(symbol: string) {
  //   this.http
  //     .get<Company[]>(
  //       'https://localhost:7221/api/CompanyInventory?symbol=${symbol}'
  //     )
  //     .subscribe(
  //       (result) => {
  //         this.companyToDisplay = result;
  //       },
  //       (error) => {
  //         console.log(error);
  //       }
  //     );
  // }

  onRowSelect(company: any) {
    this.selectedCompany = company;
    console.log(this.selectedCompany);
    this.initializeChart();
  }

  initializeChart() {
    if (!this.selectedCompany) return;

    const ctx = document.getElementById('priceGraph') as HTMLCanvasElement;
    if (!ctx) return;

    if (this.chart) {
      this.chart.destroy();
    }

    const prices =
      this.selectedCompany.history?.map((entry) => entry.price) || [];
    const labels =
      this.selectedCompany.history?.map((entry) => entry.date) || [];

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: `Price Trend`,
            data: prices,
            borderColor: '#007bff',
            borderWidth: 2,
            fill: false,
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          x: {
            beginAtZero: false,
          },
          y: {
            beginAtZero: false,
          },
        },
        layout: {
          padding: {
            top: 10,
            bottom: 10,
          },
        },
      },
    });
  }
}
