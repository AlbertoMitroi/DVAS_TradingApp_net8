import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class StripeService {
  private apiUrl = 'https://localhost:7221/api/stripe';

  constructor(private http: HttpClient) { }

  createCheckoutSession(amount: number): Observable<any> {
    const body = { amount };
    const token = localStorage.getItem('authToken');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token || ''}`
    });

    return this.http.post<any>(`${this.apiUrl}/create-checkout-session`, body, { headers }).pipe(
      tap(response => console.log('Response:', response)),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }
}
