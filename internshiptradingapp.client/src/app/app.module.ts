import { HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainDashboardModule } from './features/main-dashboard/module-config/main-dashboard.module';
import { WeatherForecastModule } from './features/weather-forecast/module-config/weather-forecast.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@NgModule({
  declarations: [
    AppComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MainDashboardModule,
    WeatherForecastModule,
    BrowserAnimationsModule,
    TableModule,
    PaginatorModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }

import { platformBrowser } from '@angular/platform-browser';
import { ChartComponent } from './features/main-dashboard/components/chart/chart.component';


platformBrowser().bootstrapModule(AppModule).catch(err => console.error(err));
