import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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
  ) {}

  ngOnInit(): void {
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
        //this.router.navigate(['/']);
      }
    }, 1000);
  }

  clearCountdown(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
}
