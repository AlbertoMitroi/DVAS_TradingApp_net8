import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BankAccount } from '../_models/bankAccount';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BankAccountService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7221/api/';

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
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
}
