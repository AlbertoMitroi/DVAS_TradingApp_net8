import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainDashboardIndexComponent } from '../pages/main-dashboard-index/main-dashboard-index.component';

const routes: Routes = [
  {
    path: 'main-dashboard',
    component: MainDashboardIndexComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainDashboardRoutingModule {}
