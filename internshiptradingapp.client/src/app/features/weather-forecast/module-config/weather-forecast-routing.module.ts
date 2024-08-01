import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WeaterForecastIndexComponent } from '../pages/weater-forecast-index/weater-forecast-index.component';

const routes: Routes = [

  {
    path: 'weather-forecast',
    component: WeaterForecastIndexComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WeatherForecastRoutingModule { }
