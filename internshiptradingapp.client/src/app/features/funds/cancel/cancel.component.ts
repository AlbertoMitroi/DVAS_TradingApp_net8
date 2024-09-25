import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cancel',
  templateUrl: './cancel.component.html',
  styleUrls: ['./cancel.component.css']
})
export class CancelComponent implements OnInit, OnDestroy {
  countdown: number = 5;
  private intervalId?: number; // Make the intervalId optional

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.startCountdown();
  }

  ngOnDestroy(): void {
    this.clearCountdown();
  }

  startCountdown(): void {
    this.intervalId = window.setInterval(() => {
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
