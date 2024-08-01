import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WeatherForecastRoutingModule } from './weather-forecast-routing.module';
import { WeaterForecastIndexComponent } from '../pages/weater-forecast-index/weater-forecast-index.component';


@NgModule({
  declarations: [
    WeaterForecastIndexComponent
  ],
  imports: [
    CommonModule,
    WeatherForecastRoutingModule
  ]
})
export class WeatherForecastModule { }
