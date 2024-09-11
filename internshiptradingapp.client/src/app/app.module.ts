import { HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainDashboardModule } from './features/main-dashboard/module-config/main-dashboard.module';

import { ChartModule } from 'primeng/chart';
import { SplitterModule } from 'primeng/splitter';
import { CarouselModule } from 'primeng/carousel';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [AppComponent],
  imports: [
    PaginatorModule,
    ChartModule,
    CarouselModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MainDashboardModule,
    InputTextModule,
    TableModule,
    SplitterModule,
    FormsModule,
  ],
  providers: [provideAnimationsAsync()],
  bootstrap: [AppComponent],
})
export class AppModule {}

import { platformBrowser } from '@angular/platform-browser';
import { ChartComponent } from './features/main-dashboard/components/chart/chart.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

platformBrowser()
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
