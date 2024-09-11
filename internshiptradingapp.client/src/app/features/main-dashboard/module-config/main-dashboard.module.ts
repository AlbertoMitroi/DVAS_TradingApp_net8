import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { MarketSearchBarComponent } from '../components/market-search-bar/market-search-bar.component';
import { CarouselOfSymbolsComponent } from '../components/carousel-of-symbols/carousel-of-symbols.component';
import { MarketTableComponent } from '../components/market-table/market-table.component';
import { TopXTableComponent } from '../components/top-xtable/top-xtable.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ChartModule } from 'primeng/chart';
import { InputTextModule } from 'primeng/inputtext';
import { SidebarModule } from 'primeng/sidebar';
import { SplitterModule } from 'primeng/splitter';
import { CarouselModule } from 'primeng/carousel';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
  declarations: [
    MainDashboardIndexComponent,
    MarketSearchBarComponent,
    CarouselOfSymbolsComponent,
    MarketTableComponent,
    TopXTableComponent,
  ],
  imports: [
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatSlideToggleModule,
    FormsModule,
    TableModule,
    CommonModule,
    MainDashboardRoutingModule,
    ButtonModule,
    SidebarModule,
    InputTextModule,
    ChartModule,
    SplitterModule,
    BrowserAnimationsModule,
    CarouselModule,
  ],
  providers: [],
  bootstrap: [MainDashboardModule],
})
export class MainDashboardModule {}
