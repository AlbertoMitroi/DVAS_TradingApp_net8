import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../_models/user';
import { tap, map } from 'rxjs/operators';
import { BankAccount } from '../_models/bankAccount';
import { RegisterDto } from '../_models/RegisterDto';
import { UserDetailsDto } from '../_models/userDetailsDto';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  public userHub = inject(SignalRService);
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7221/api/';
  currentUser = signal<User | null>(null);
  
  private balanceSubject = new BehaviorSubject<string | null>(null);
  balance$ = this.balanceSubject.asObservable();

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  constructor() {
    this.initializeSignalR();
  }

  private initializeSignalR() {
    const user = this.currentUser();
    if (user) {
      this.userHub.createHubConnection(user);
    }

    this.userHub.userDetails$.subscribe(userDetails => {
      if (userDetails) {
        this.balanceSubject.next(userDetails.balance);
      }
    });
  }
  

  get getUserDetails$(): Observable<UserDetailsDto | null> {
    return this.userHub.userDetails$;
  }

  getBankAccounts(): Observable<BankAccount[]> {
    return this.http.get<BankAccount[]>(this.baseUrl + 'BankAccount', {
      headers: this.getAuthHeaders()
    });
  }

  addBankAccount(iban: string, bankName: string): Observable<string> {
    const body = { iban, bankName };
    return this.http.post<string>(this.baseUrl + 'BankAccount', body, {
      headers: this.getAuthHeaders(),
      responseType: 'text' as 'json'
    });
  }

  addFunds(amount: number): Observable<void> {
    const body = { amount };
    return this.http.post<void>(this.baseUrl + 'Funds/add', body, {
      headers: this.getAuthHeaders()
    }).pipe(
      tap(() => {
      })
    );
  }

  withdrawFunds(bankAccountId: number, amount: number): Observable<void> {
    const body = { bankAccountId, amount };
    return this.http.post<void>(this.baseUrl + 'Funds/withdraw', body, {
      headers: this.getAuthHeaders()
    }).pipe(
      tap(() => {
      })
    );
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          localStorage.setItem('authToken', user.token);
          this.currentUser.set(user);
          this.userHub.createHubConnection(user);
        }
        return user;
      })
    );
  }

  register(registerDto: RegisterDto): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'account/register', registerDto).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          localStorage.setItem('authToken', user.token);
          this.currentUser.set(user);
          this.userHub.createHubConnection(user);
        }
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
    this.userHub.stopHubConnection();
  }

  transformUsername(username: string): string {
    return username.split('_').map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');
  }
}
