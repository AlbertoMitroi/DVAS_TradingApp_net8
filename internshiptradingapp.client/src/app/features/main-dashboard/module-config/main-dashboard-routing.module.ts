import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { MarketsComponent } from '../pages/markets/markets.component';

const routes: Routes = [
  {
    path: 'main-dashboard',
    component: MainDashboardIndexComponent
  },
  {
    path: 'markets',
    component:MarketsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainDashboardRoutingModule { }
