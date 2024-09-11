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
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
  declarations: [
    AppComponent,
    ChartComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MainDashboardModule,
    WeatherForecastModule,
    BrowserAnimationsModule,
    TableModule,
    PaginatorModule,
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatSlideToggleModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }

import { platformBrowser } from '@angular/platform-browser';
import { ChartComponent } from './features/main-dashboard/components/chart/chart.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


platformBrowser().bootstrapModule(AppModule).catch(err => console.error(err));
