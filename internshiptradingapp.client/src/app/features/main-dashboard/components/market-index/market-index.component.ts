import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { HttpClient, HttpParams } from '@angular/common/http';

interface marketIndexEntry {
  date: Date;
  value: number;
}

@Component({
  selector: 'app-market-index',
  templateUrl: './market-index.component.html',
  styleUrl: './market-index.component.css',
})
export class MarketIndexComponent {
  private chart: Chart | null = null;
  public marketIndex: number = 0;
  marketIndexEntries: marketIndexEntry[] = [];

  constructor(private http: HttpClient) { }
  ngOnInit() {
    this.getMarketIndexEntries();
    setTimeout(() => this.initializeChart(), 1);
  }

  getMarketIndexEntries() {
    let params = new HttpParams();

    console.log('API Parameters:', params.toString());
    this.http
      .get<marketIndexEntry[]>(
        'https://localhost:7221/api/CompanyInventory/marketIndexHistory',
        { params }
      )
      .subscribe(
        (result) => {
          this.marketIndexEntries = result.reverse();

          setTimeout(() => this.initializeChart(), 1);
        },
        (error) => {
          console.log(error);
        }
      );
  }

  initializeChart() {
    const ctx = document.getElementById(
      'marketIndexGraph'
    ) as HTMLCanvasElement;
    if (!ctx) return;

    ctx.width = ctx.parentElement?.clientWidth || 600;
    ctx.height = ctx.parentElement?.clientHeight || 250;

    if (Chart.getChart('marketIndexGraph')) {
      Chart.getChart('marketIndexGraph')?.destroy();
    }

    const prices =
      this.marketIndexEntries.slice(-7).map((entry) => entry.value) || [];
    const labels = this.marketIndexEntries.slice(-7).map((entry) => {
      return new Date(entry.date).toISOString().split('T')[0];
    });

    this.marketIndex = prices[prices.length - 1];

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Market Index',
            data: prices,
            borderColor: '#536B78',
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
}
