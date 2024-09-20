import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from './_services/auth.service'; 
import { SignalRService } from './_services/signal-r.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Corectat stilizarea
})
export class AppComponent implements OnInit {
  private authService = inject(AuthService);
  private signalRService = inject(SignalRService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    
    // Verifică dacă nu există utilizator
    if (!userString) return;

    // Parsează utilizatorul
    const user: User = JSON.parse(userString);

    // Setează utilizatorul în AuthService
    this.authService.currentUser.set(user);

    // Inițializează conexiunea SignalR dacă utilizatorul există și are un token valid
    if (user && user.token) {
      this.signalRService.initializeSignalRConnections(user);
    } else {
      console.error('Invalid user or missing token');
    }
  }
}
