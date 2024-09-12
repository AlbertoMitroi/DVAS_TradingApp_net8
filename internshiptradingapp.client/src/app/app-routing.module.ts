import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { RegisterComponent } from './features/register/register.component';
import { LoginComponent } from './features/login/login.component';
import { PaymentComponent } from './features/funds/payment/payment.component';
import { SuccessComponent } from './features/funds/success/success.component';
import { CancelComponent } from './features/funds/cancel/cancel.component';
import { WithdrawComponent } from './features/funds/withdraw/withdraw.component';
import { AddBankComponent } from './features/funds/add-bank/add-bank.component';
import { TransactionsComponent } from './features/funds/transactions/transactions.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'success', component: SuccessComponent },
  { path: 'cancel', component: CancelComponent },
  { path: 'withdraw', component: WithdrawComponent },
  { path: 'add-bank', component: AddBankComponent },
  { path: 'transactions', component: TransactionsComponent },

  {
    pathMatch: 'full',
    path: '',
    redirectTo: 'main-dashboard',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
