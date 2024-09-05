import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { MainDashboardModule } from "./features/main-dashboard/module-config/main-dashboard.module";
import { WeatherForecastModule } from "./features/weather-forecast/module-config/weather-forecast.module";


@NgModule({
    declarations: [
        AppComponent,

    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        MainDashboardModule,
        WeatherForecastModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
