import { Component } from '@angular/core';
import { Chart } from 'chart.js';

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

  ngOnInit() {
    setTimeout(() => this.initializeChart(), 1);
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

    // const prices =
    //   this.selectedCompany.history?.map((entry) => entry.price) || [];
    // const labels =
    //   this.selectedCompany.history?.map((entry) => entry.date) || [];

    let values = [10, 20, 10, 30, 40];

    this.marketIndex = values[values.length - 1];

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: ['M', 'T', 'W', 'T', 'F'],
        datasets: [
          {
            label: 'Market Index',
            data: values,
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
}
