import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-weater-forecast-index',
  templateUrl: './weater-forecast-index.component.html',
  styleUrl: './weater-forecast-index.component.css'
})
export class WeaterForecastIndexComponent {
  public forecasts: WeatherForecast[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'internshiptradingapp.client';
}
