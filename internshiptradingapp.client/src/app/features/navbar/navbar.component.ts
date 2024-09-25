import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalRService } from '../../_services/signal-r.service';
import { AuthService } from '../../_services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  username: string | null = null;
  balance: string | null = null;
  
  private userSubscription: Subscription | null = null;

  constructor(
    public authService: AuthService,
    private signalRService: SignalRService, 
    private router: Router 
  ) {}

  ngOnInit() {
    this.userSubscription = this.signalRService.userDetails$.subscribe({
      next: (userDetails) => {
        if (userDetails) {
          this.username = this.signalRService.transformUsername(userDetails.username);
          this.balance = userDetails.balance;
        }
      },
      error: (err) => {
        console.error('Failed to load user details', err);
        this.username = 'Username not loaded';
        this.balance = "$ ERROR";
      }
    });
  }

  ngOnDestroy() {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }

  reloadPage(event: Event) {
    event.preventDefault();
    window.location.href = '/';
    //this.router.navigate(['/']);
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }
}
