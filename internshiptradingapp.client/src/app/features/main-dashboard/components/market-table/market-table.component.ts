import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, AfterViewInit, HostListener, ViewChild } from '@angular/core';
import { Chart } from 'chart.js';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SharedCompanyService } from '../../services/shared-company/shared-company.service';

export interface HistoryEntry {
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
  volume: number;
}

export interface CompanyDetails {
  id: number;
  name: string;
  symbol: string;
  status: number;
  history: HistoryEntry[] | null;
}

export interface CompanyObject {
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
  public selectedCompany: CompanyObject | null = null;
  private chart: Chart | null = null;
  private ask: any | null = null;
  companyAttributes: string[] = ['Price', 'Volume', 'Day Variation', 'Name', 'Symbol'];
  selectedAttribute: string | null = null;

  // DataSource for MatTable
  public dataSource = new MatTableDataSource<CompanyObject>([]);

  // ViewChild to access the paginator component
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private http: HttpClient,
    private sharedCompanyService: SharedCompanyService
  ) { }

  ngOnInit() {
    this.getCompanies();
    this.sharedCompanyService.selectedCompany$.subscribe((receivedValue) => {
      this.ask = receivedValue;
      console.log('Received value:', this.ask);
      this.onRowSelect(this.ask);
    });
    console.log(this.companies);
  }

  ngAfterViewInit() {
    // Attach the paginator after the view has been initialized
    this.dataSource.paginator = this.paginator;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isMobileView = window.innerWidth < 768;
  }

  getCompanies(sortAttribute?: string) {
    let params = new HttpParams();

    if (sortAttribute) {
      params = params.set('value', sortAttribute);
    }

    params = params.set('orderToggle', 'desc');
    this.http
      .get<CompanyObject[]>(
        'https://localhost:7221/api/CompanyInventory/topXCompaniesByParameter',
        { params }
      )
      .subscribe(
        (result) => {
          this.companies = result;
          console.log(this.companies);

          // Populate the MatTableDataSource with the companies
          this.dataSource.data = this.companies;

          console.log('Received response from server');

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

  onSearch(companyData: any) {
    if (companyData) {
      this.selectedCompany = companyData;
      this.initializeChart();
    }
  }

  onRowSelect(company: any) {
    this.selectedCompany = company;
    console.log(this.selectedCompany);
    this.initializeChart();
  }

  initializeChart() {
    if (!this.selectedCompany) return;

    const ctx = document.getElementById('priceGraph') as HTMLCanvasElement;
    if (!ctx) return;

    ctx.width = ctx.parentElement?.clientWidth || 600;
    ctx.height = ctx.parentElement?.clientHeight || 250;

    if (this.chart) {
      this.chart.destroy();
    }

    const prices = this.selectedCompany.history?.map((entry) => entry.price) || [];
    const labels = this.selectedCompany.history?.map((entry) => entry.date) || [];

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Price Trend',
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
          x: { beginAtZero: false },
          y: { beginAtZero: false },
        },
      },
    });
  }

  // Handle paginator page changes
  onPaginateChange(event: any) {
    const pageIndex = event.pageIndex;
    const pageSize = event.pageSize;

    // Log the page change (you can implement further logic here if needed)
    console.log(`Page Index: ${pageIndex}, Page Size: ${pageSize}`);
  }
}
