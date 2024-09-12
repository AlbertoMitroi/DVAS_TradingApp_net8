import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../_services/account.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-add-bank',
  templateUrl: './add-bank.component.html',
  styleUrls: ['./add-bank.component.css']
})
export class AddBankComponent implements OnInit {
  iban: string = '';
  bankName: string = '';
  message: string | null = null;
  isLoading: boolean = false;
  returnUrl: string = '';
  showRedirectMessage: boolean = false;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params['returnUrl'] || null;

      if (this.returnUrl) {
        setTimeout(() => {
          this.messageService.add({
            key: 'global',
            severity: 'info',
            summary: 'Redirected to Add Bank',
            detail: `You were redirected from another page to add a new bank account.`
          });
        }, 0);
      }
    });
  }
  

  addBankAccount() {
    if (this.iban && this.bankName) {
      this.isLoading = true;
      this.accountService.addBankAccount(this.iban, this.bankName).subscribe(
        () => {
          this.message = 'Bank account added successfully.';
          this.messageService.add({
            key: 'global',
            severity: 'info',
            summary: 'Success',
            detail: 'Bank account added successfully.'
          });
          if (this.returnUrl != null) {
            this.showRedirectMessage = true;
            this.messageService.add({
              key: 'global',
              severity: 'info',
              summary: 'Success',
              detail: 'Bank account added successfully. Redirecting...'
            });
            setTimeout(() => {
              this.isLoading = false;
              this.router.navigate([this.returnUrl]);
            }, 3000);
          }
          this.isLoading = false;
        },
        err => {
          console.error('Failed to add bank account', err);
          this.message = 'Failed to add bank account. Please try again.';
          this.isLoading = false;
        }
      );
    } else {
      this.message = 'Please fill in both fields.';
    }
  }
}
