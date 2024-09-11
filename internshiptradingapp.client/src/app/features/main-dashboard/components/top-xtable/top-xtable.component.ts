import { Component, HostListener } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chart } from 'chart.js';

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
  styleUrl: './top-xtable.component.css'
})
export class TopXTableComponent {
  isMobileView: boolean = window.innerWidth < 768;
  public companies: any[] = [];
  public selectedCompany: any | null = null;
  private chart: Chart | null = null;

  constructor(private http: HttpClient) { }

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
    this.http
      .get<Company[]>(
        'https://localhost:7221/api/CompanyInventory/topXCompaniesByParameter'
      )
      .subscribe(
        (result) => {
          this.companies = result;
          if (this.companies.length > 0) {
            this.selectedCompany = this.companies[0];
            setTimeout(() => this.initializeChart(), 100);
          }
        },
        (error) => {
          console.log(error);
        }
      );
  }

  onRowSelect(company: any) {
    this.selectedCompany = company;
    this.initializeChart();
  }

  initializeChart() {
    if (!this.selectedCompany) return;

    const canvasElement = document.getElementById('priceGraph') as HTMLCanvasElement;
    const simulatedPrices = this.getSimulatedPriceDataForCompany(this.selectedCompany);

    if (this.chart) {
      this.chart.data.datasets[0].data = simulatedPrices; 
      this.chart.data.labels = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
      this.chart.update(); 
    } else {
     
      this.chart = new Chart(canvasElement, {
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
  }



  getSimulatedPriceDataForCompany(company: any): number[] {
    const basePrice = company.price || 100;
    return Array.from({ length: 7 }, () =>
      basePrice + (Math.random() - 0.5) * 10
    );
  }
}

