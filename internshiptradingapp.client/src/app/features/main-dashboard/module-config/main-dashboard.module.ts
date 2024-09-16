import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { MarketSearchBarComponent } from '../components/market-search-bar/market-search-bar.component';
import { CarouselOfSymbolsComponent } from '../components/carousel-of-symbols/carousel-of-symbols.component';
import { MarketTableComponent } from '../components/market-table/market-table.component';
import { TopXTableComponent } from '../components/top-xtable/top-xtable.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ChartModule } from 'primeng/chart';
import { InputTextModule } from 'primeng/inputtext';
import { SidebarModule } from 'primeng/sidebar';
import { SplitterModule } from 'primeng/splitter';
import { CarouselModule } from 'primeng/carousel';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatListModule } from '@angular/material/list';



@NgModule({
  declarations: [
    MainDashboardIndexComponent,
    CarouselOfSymbolsComponent,
    MarketTableComponent,
    TopXTableComponent,
    MarketSearchBarComponent
  ],
  imports: [
    MatFormFieldModule,
    MatListModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatIconModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatInputModule,
    MatTableModule,
    MatDividerModule,
    MatCardModule,
    MatButtonModule,
    MatSlideToggleModule,
    FormsModule,
    TableModule,
    CommonModule,
    DropdownModule,
    MainDashboardRoutingModule,
    ButtonModule,
    SidebarModule,
    InputTextModule,
    ChartModule,
    SplitterModule,
    BrowserAnimationsModule,
    CarouselModule,
  ],
  exports: [
    MainDashboardIndexComponent
  ],
  providers: [],
  bootstrap: [MainDashboardModule],
})
export class MainDashboardModule {}
