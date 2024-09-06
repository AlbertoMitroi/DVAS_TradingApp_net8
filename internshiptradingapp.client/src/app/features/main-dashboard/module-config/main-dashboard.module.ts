import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CarouselOfSymbolsComponent } from "../components/carousel-of-symbols/carousel-of-symbols.component";

import { MarketsComponent } from '../pages/markets/markets.component';
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { ChartModule } from "primeng/chart";
import { InputTextModule } from "primeng/inputtext";
import { SidebarModule } from "primeng/sidebar";
import { SplitterModule } from "primeng/splitter";
import { CarouselModule } from 'primeng/carousel';



@NgModule({
  declarations: [   
    MainDashboardIndexComponent,
    MarketsComponent,
    CarouselOfSymbolsComponent
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
    CarouselModule
  ],
  providers: [],
  bootstrap: [MainDashboardModule]
})
export class MainDashboardModule { }
