
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './features/navbar/navbar.component';
import { MenubarModule } from 'primeng/menubar';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { RegisterComponent } from './features/register/register.component';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { PaymentComponent } from './features/funds/payment/payment.component';
import { SuccessComponent } from './features/funds/success/success.component';
import { CancelComponent } from './features/funds/cancel/cancel.component';
import { WithdrawComponent } from './features/funds/withdraw/withdraw.component';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { AddBankComponent } from './features/funds/add-bank/add-bank.component';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { TransactionsComponent } from './features/funds/transactions/transactions.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { ChartModule } from 'primeng/chart';
import { SplitterModule } from 'primeng/splitter';
import { CarouselModule } from 'primeng/carousel';
import { MainDashboardModule } from './features/main-dashboard/module-config/main-dashboard.module';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    HomeComponent,
    LoginComponent,
    PaymentComponent,
    SuccessComponent,
    CancelComponent,
    WithdrawComponent,
    AddBankComponent,
    TransactionsComponent,
    OrderWithTableComponent,
  ],
  imports: [
    MainDashboardModule,
    NgxPaginationModule,
    ToastModule,
    DialogModule,
    HttpClientModule,
    ChartModule,
    CarouselModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    DropdownModule,
    MenubarModule,
    TabsModule,
    BsDropdownModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ButtonModule,
    PasswordModule,
    CheckboxModule,
    InputTextModule,
    MessageModule,
    ReactiveFormsModule,
    InputTextModule,
    SplitterModule,
    FormsModule,
  ],
  exports:[
  ],
    providers: [MessageService, provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule {}

import { platformBrowser } from '@angular/platform-browser';
import { ChartComponent } from './features/main-dashboard/components/chart/chart.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { OrderWithTableComponent } from './features/main-dashboard/components/orders/order-with-table/order-with-table.component';
platformBrowser()
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
