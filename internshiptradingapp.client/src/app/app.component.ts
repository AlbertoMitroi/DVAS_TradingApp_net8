import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from './_services/auth.service'; 
import { SignalRService } from './_services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
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
    const user = JSON.parse(userString);
    this.authService.currentUser.set(user);
    if (user) {
      this.signalRService.initializeSignalRConnections(user);
    }
  }
}
