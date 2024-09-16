import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BankAccountService } from '../../../_services/bank-account.service'; 
import { FundsService } from '../../../_services/funds.service';
import { BankAccount } from '../../../_models/bankAccount';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  styleUrls: ['./withdraw.component.css']
})
export class WithdrawComponent implements OnInit {
  bankAccounts: BankAccount[] = [];
  selectedBankAccountId: number | null = null;
  amount: number | null = null;
  message: string | null = null;
  isBankAccountDialogVisible: boolean = false;
  isLoading: boolean = false;

  constructor(
    private bankAccountService: BankAccountService, 
    private fundsService: FundsService,
    private router: Router,
    private messageService: MessageService) {}

  ngOnInit() {
    this.loadBankAccounts();
  }

  loadBankAccounts() {
    this.bankAccountService.getBankAccounts().subscribe(
      accounts => this.bankAccounts = accounts,
      err => console.error('Failed to load bank accounts', err)
    );
  }

  openBankAccountDialog() {
    this.isBankAccountDialogVisible = true;
  }

  selectBankAccount(id: number) {
    this.selectedBankAccountId = id;
    this.isBankAccountDialogVisible = false;
  }

  getSelectedBankName(): string {
    const selectedAccount = this.bankAccounts.find(account => account.id === this.selectedBankAccountId);
    return selectedAccount ? selectedAccount.bankName : '';
  }

  async withdrawFunds() {
    if (this.selectedBankAccountId && this.amount) {
      console.log(this.selectedBankAccountId)
      this.isLoading = true;
      await this.wait(3000);
      this.isLoading = false;
      this.fundsService.withdrawFunds(this.selectedBankAccountId, this.amount).subscribe(
        () => {
          this.router.navigate(['/success']);
          this.message = 'Withdrawal successful.';
        },
        err => {
          this.router.navigate(['/cancel']);
            console.error('Failed to withdraw funds', err);
            this.message = 'Failed to withdraw funds. Please try again.';
        }
      );
    } else {
      this.message = 'Please select a bank account and enter a valid amount.';
    }
  }

  private wait(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  addNewBank() {
    this.router.navigate(['/add-bank'], { queryParams: { returnUrl: this.router.url } });
  }
}
