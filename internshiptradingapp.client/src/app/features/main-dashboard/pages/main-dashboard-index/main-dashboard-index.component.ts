import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, AfterViewInit, HostListener } from '@angular/core';
import { Chart } from 'chart.js';

interface Company {
  name: string;
  symbol: string;
  price: number;
  referencePrice: number;
  openingPrice: number;
  closingPrice: number;
  eps: number;
  per: number;
  dayVariation: number;
  status: number;
}

@Component({
  selector: 'app-main-dashboard-index',
  templateUrl: './main-dashboard-index.component.html',
  styleUrls: ['./main-dashboard-index.component.css'],
})
export class MainDashboardIndexComponent {
  isMobileView: boolean = window.innerWidth < 768;
  public companies: Company[] = [];
  public expandedRows: { [key: number]: boolean } = {};
  public selectedCompany: Company | null = null;
  public splitCompanies: Company[][] = [];
  private chart: Chart | null = null;

  constructor(private http: HttpClient) { }

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
      .get<Company[]>('https://localhost:7221/api/CompanyInventory/most-traded')
      .subscribe(
        (result) => {
          this.companies = result.slice(0, 10);
          this.splitCompanies = [
            this.companies.slice(0, 5),
            this.companies.slice(5),
          ];
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

  onRowSelect(company: Company) {
    this.selectedCompany = company;
    this.initializeChart();
  }

  initializeChart() {
    if (!this.selectedCompany) return;

    const ctx = document.getElementById('priceGraph') as HTMLCanvasElement;
    if (!ctx) return;

    const simulatedPrices = this.getSimulatedPriceDataForCompany(
      this.selectedCompany
    );

    if (this.chart) {
      this.chart.destroy();
    }

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        datasets: [
          {
            label: `Price Trend`,
            data: simulatedPrices,
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
            beginAtZero: true,
          },
          y: {
            beginAtZero: true,
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

  getSimulatedPriceDataForCompany(company: Company): number[] {
    const basePrice = company.price;
    return Array.from(
      { length: 7 },
      () => basePrice + (Math.random() - 0.5) * 10
    );
  }
}
