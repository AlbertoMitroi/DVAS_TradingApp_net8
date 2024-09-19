import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';
import { MarketIndexComponent } from '../components/market-index/market-index.component';

const routes: Routes = [
  {
    path: 'main-dashboard',
    component: MainDashboardIndexComponent,
  },
  { path: 'market-index', component: MarketIndexComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainDashboardRoutingModule {}
