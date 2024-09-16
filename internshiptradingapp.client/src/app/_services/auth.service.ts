import { Injectable, inject, signal, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { RegisterDto } from '../_models/RegisterDto';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7221/api/';
  currentUser = signal<User | null>(null);

  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
  }

  login(model: any): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          this.setUser(user);
        }
        return user;
      })
    );
  }

  register(registerDto: RegisterDto): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'account/register', registerDto).pipe(
      map(user => {
        if (user) {
          this.setUser(user);
        }
        return user;
      })
    );
  }

  logout(): void {
    localStorage.removeItem('user');
    localStorage.removeItem('authToken');
    this.currentUser.set(null);
    this.signalRService.stopAllHubConnections(); 
  }

  private setUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    localStorage.setItem('authToken', user.token);
    this.currentUser.set(user);
  }
}
