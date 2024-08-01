import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainDashboardRoutingModule } from './main-dashboard-routing.module';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';


@NgModule({
  declarations: [   
    MainDashboardIndexComponent
  ],
  imports: [
    CommonModule,
    MainDashboardRoutingModule
  ]
})
export class MainDashboardModule { }
