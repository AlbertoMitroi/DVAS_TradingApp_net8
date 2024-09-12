import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  accountService = inject(AccountService);
  private router = inject(Router);
  username: string | null = null;
  balance: string | null = null;
  private userSubscription: Subscription | null = null;
  private balanceSubscription: Subscription | null = null;

  ngOnInit() {
    const user = this.accountService.currentUser();
    if (user) {
      this.accountService.userHub.createHubConnection(user);
    }

    this.userSubscription = this.accountService.getUserDetails$.subscribe({
      next: (userDetails) => {
        if (userDetails) {
          this.username = this.accountService.transformUsername(userDetails.username);
          this.balance = userDetails.balance;
        }
      },
      error: (err) => {
        console.error('Failed to load user details', err);
        this.username = 'Username not loaded';
        this.balance = "$ ERROR";
      }
    });

    this.balanceSubscription = this.accountService.balance$.subscribe({
      next: (balance) => {
        if (balance !== null) {
          this.balance = balance;
        }
      }
    });
  }

  ngOnDestroy() {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
    if (this.balanceSubscription) {
      this.balanceSubscription.unsubscribe();
    }
  }

  reloadPage(event: Event) {
    event.preventDefault();
    window.location.href = '/';
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }
}
