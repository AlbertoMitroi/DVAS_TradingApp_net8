import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../_services/account.service';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent implements OnInit, OnDestroy {
  countdown: number = 5;
  private intervalId?: any;
  amount: string | null = null;
  userId: string | null = null;
  username: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    // Extract query params and perform actions
    this.route.queryParams.subscribe(params => {
      this.amount = params['amount'];
      this.userId = params['userId'];
      this.username = params['username'];

      if (this.amount) {
        this.addFunds(parseFloat(this.amount));
      }
    });

    // Start countdown
    this.startCountdown();
  }

  ngOnDestroy(): void {
    this.clearCountdown();
  }

  startCountdown(): void {
    this.intervalId = setInterval(() => {
      this.countdown--;
      if (this.countdown <= 0) {
        this.clearCountdown();
        window.location.href = '/';
      }
    }, 1000);
  }

  clearCountdown(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  private addFunds(amount: number): void {
    this.accountService.addFunds(amount).subscribe(
      () => {
        console.log('Funds added successfully.');
      },
      err => {
        console.error('Failed to add funds', err);
      }
    );
  }
}
