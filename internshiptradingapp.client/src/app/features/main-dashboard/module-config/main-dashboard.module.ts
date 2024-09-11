import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CarouselOfSymbolsComponent } from "../components/carousel-of-symbols/carousel-of-symbols.component";
import { PaginatorModule } from 'primeng/paginator';
import { MarketsComponent } from '../pages/markets/markets.component';
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { ChartModule } from "primeng/chart";
import { InputTextModule } from "primeng/inputtext";
import { SidebarModule } from "primeng/sidebar";
import { SplitterModule } from "primeng/splitter";
import { CarouselModule } from 'primeng/carousel';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TopXTableComponent } from '../components/top-xtable/top-xtable.component';



@NgModule({
  declarations: [   
    MainDashboardIndexComponent,
    MarketsComponent,
    CarouselOfSymbolsComponent,
    TopXTableComponent,
  ],
  imports: [
    CommonModule,
    MainDashboardRoutingModule,
    FormsModule,
    ButtonModule,
    SidebarModule,
    InputTextModule,
    ChartModule,
    SplitterModule,
    BrowserAnimationsModule,
    CarouselModule,
    TableModule,
    PaginatorModule,
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatSlideToggleModule,
  ],
  providers: [],
  bootstrap: [MainDashboardModule]
})
export class MainDashboardModule { }
