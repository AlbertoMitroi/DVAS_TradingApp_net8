import { Component } from '@angular/core';
import { StripeService } from '../../../_services/stripe.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  amount: number = 100;
  isLoading: boolean = false;


  constructor(private stripeService: StripeService
  ) {}

  createCheckoutSession(): void {
    this.isLoading = true;
    this.stripeService.createCheckoutSession(this.amount).subscribe({
      next: (response: any) => {
        window.location.href = response.url;
      },
      error: (error) => console.error('Error creating checkout session', error)
    });
  }
}
