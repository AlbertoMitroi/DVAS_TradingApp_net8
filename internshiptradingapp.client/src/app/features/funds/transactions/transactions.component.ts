import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'; // Import ActivatedRoute
import { AccountService } from '../../../_services/account.service'; // Adjust path as needed
import { Transaction } from '../../../_models/userDetailsDto'; // Adjust path as needed

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
  transactions: Transaction[] = []; // Use the Transaction interface from the service
  pWithdraw: number = 1;
  pAll: number = 1;
  pDeposit: number = 1;
  pageSize: number = 3;
  maxSize: number = 5;

  selectedTab: string = 'all';

  filteredWithdrawTransactions: Transaction[] = [];
  filteredDepositTransactions: Transaction[] = [];
  filteredAllTransactions: Transaction[] = [];

  constructor(
    private accountService: AccountService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.checkRouteAndSetTab(); 
    this.loadTransactions();
  }

  private checkRouteAndSetTab(): void {
    this.route.url.subscribe(urlSegments => {
      if (urlSegments.some(segment => segment.path === 'payment')) {
        this.selectedTab = 'deposit';
      }
      if (urlSegments.some(segment => segment.path === 'withdraw')) {
        this.selectedTab = 'withdraw';
      }
      if (urlSegments.some(segment => segment.path === 'transactions')) {
        this.selectedTab = 'all';
        this.pageSize = 10;
        this.maxSize = 12;
      }
    });
  }

  loadTransactions(): void {
    this.accountService.getUserDetails$.subscribe(userDetails => {
      if (userDetails) {
        this.transactions = userDetails.transactions;
        this.filterTransactions();
      }
      this.filterTransactions();
    });
  }

  filterTransactions(): void {
    this.filteredWithdrawTransactions = this.transactions.filter(
      (t) => t.type === 'Withdraw'
    );
    this.filteredDepositTransactions = this.transactions.filter(
      (t) => t.type === 'Deposit'
    );
    this.filteredAllTransactions = this.transactions;
  }

  selectTab(tab: string): void {
    this.selectedTab = tab;
    if (tab === 'withdraw') this.pWithdraw = 1;
    if (tab === 'all') this.pAll = 1;
    if (tab === 'deposit') this.pDeposit = 1;
  }
}
