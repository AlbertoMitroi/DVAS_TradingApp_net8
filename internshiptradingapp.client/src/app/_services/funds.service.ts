import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FundsService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7221/api/';

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  addFunds(amount: number): Observable<void> {
    const body = { amount };
    return this.http.post<void>(this.baseUrl + 'Funds/add', body, {
      headers: this.getAuthHeaders()
    });
  }

  withdrawFunds(bankAccountId: number, amount: number): Observable<void> {
    const body = { bankAccountId, amount };
    return this.http.post<void>(this.baseUrl + 'Funds/withdraw', body, {
      headers: this.getAuthHeaders()
    });
  }
}
