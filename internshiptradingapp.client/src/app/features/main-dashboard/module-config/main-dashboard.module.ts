import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { MarketsComponent } from '../pages/markets/markets.component';
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { ChartModule } from "primeng/chart";
import { InputTextModule } from "primeng/inputtext";
import { SidebarModule } from "primeng/sidebar";
import { SplitterModule } from "primeng/splitter";



@NgModule({
  declarations: [   
    MainDashboardIndexComponent,
    MarketsComponent
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
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [MainDashboardModule]
})
export class MainDashboardModule { }
