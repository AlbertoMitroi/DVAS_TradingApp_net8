import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from './_services/auth.service'; 
import { SignalRService } from './_services/signal-r.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private authService = inject(AuthService);
  private signalRService = inject(SignalRService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    
    if (!userString) return;

    const user: User = JSON.parse(userString);

    this.authService.currentUser.set(user);

    if (user && user.token) {
      this.signalRService.initializeSignalRConnections(user);
    } else {
      console.error('Invalid user or missing token');
    }
  }
}
