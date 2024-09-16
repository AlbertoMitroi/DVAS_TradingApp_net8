import { HttpClient, HttpParams } from '@angular/common/http';
import {
  Component,
  AfterViewInit,
  HostListener,
  ViewChild,
} from '@angular/core';
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';

import { Chart } from 'chart.js';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SharedCompanyService } from '../../services/shared-company/shared-company.service';

import { SortableColumn } from 'primeng/table';

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
  animations: [
    trigger('fadeInOut', [
      state(
        'visible',
        style({
          opacity: 1,
          transform: 'translateY(0)',
        })
      ),
      state(
        'hidden',
        style({
          opacity: 0,
          transform: 'translateY(-20px)',
        })
      ),
      transition('visible <=> hidden', [animate('300ms ease-in-out')]),
    ]),
  ],
})
export class MarketTableComponent {
  isMobileView: boolean = window.innerWidth < 768;
  public companies: CompanyObject[] = [];
  public expandedRows: { [key: number]: boolean } = {};
  public selectedCompany: CompanyObject | null = null;
  private chart: Chart | null = null;
  private sharedCompany: any | null = null;
  sortOrder: string = 'desc';
  selectedRow: any;
  showMarketIndex: boolean | null = null;
  displayedColumns: string[] = ['symbol', 'name', 'price'];
  companyAttributes: string[] = [
    'Price',
    'Volume',
    'Day Variation',
    'Name',
    'Symbol',
  ];
  selectedAttribute: string | null = null;
  public dataSource = new MatTableDataSource<CompanyObject>([]);

  constructor(
    private http: HttpClient,
    private sharedCompanyService: SharedCompanyService
  ) {}

  // ViewChild to access the paginator component
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit() {
    this.getCompanies();
    this.sharedCompanyService.selectedCompany$.subscribe((receivedValue) => {
      this.sharedCompany = receivedValue;
      console.log('Received value:', this.sharedCompany);
      this.onRowSelect(this.sharedCompany);
    });
    this.showMarketIndex = true;
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

  toggleSortOrder() {
    // Toggle between ascending and descending
    this.sortOrder = this.sortOrder === 'desc' ? 'asc' : 'desc';

    // Call getCompanies with the updated sortOrder and selected attribute
    this.getCompanies(this.selectedAttribute, this.sortOrder);
  }
  getCompanies(sortAttribute?: string | null, sortOrder: string = 'desc') {
    let params = new HttpParams();

    // Only set 'value' if sortAttribute is not null
    if (sortAttribute) {
      params = params.set('value', sortAttribute);
    }

    params = params.set('orderToggle', sortOrder); // Add the order param
    console.log('API Parameters:', params.toString());
    this.http
      .get<CompanyObject[]>(
        'https://localhost:7221/api/CompanyInventory/topXCompaniesByParameter',
        { params }
      )
      .subscribe(
        (result) => {
          this.companies = result;
          // Populate the MatTableDataSource with the companies
          this.dataSource.data = this.companies;

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

  getSelectedCompany() {
    if (this.showMarketIndex) {
      this.showMarketIndex = false;
    }
    return this.selectedCompany;
  }

  setSelectedCompany(company: any) {
    if (this.showMarketIndex) {
      this.showMarketIndex = false;
    }
    this.selectedCompany = company;
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
  onSearch(companyData: any) {
    if (companyData) {
      //this.selectedCompany = companyData;
      this.setSelectedCompany(companyData);
      this.initializeChart();
    }
  }

  swapRightPanelContent() {
    this.showMarketIndex = !this.showMarketIndex;
    setTimeout(() => this.initializeChart(), 1);
  }
  onRowSelect(company: any) {
    //this.selectedCompany = company;
    this.setSelectedCompany(company);
    this.selectedRow = company;
    console.log(this.selectedCompany);
    setTimeout(() => this.initializeChart(), 1);
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
