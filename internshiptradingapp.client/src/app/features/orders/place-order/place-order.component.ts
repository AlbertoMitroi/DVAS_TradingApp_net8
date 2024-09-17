import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../_services/order.service';
import { SignalRService } from '../../../_services/signal-r.service';
import { CompanyWithHistoryGetDTO } from '../../../_models/CompanyWithHistoryGetDTO';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})
export class PlaceOrderComponent implements OnInit {
  stockSymbol: string = '';
  quantity: number | null = null;
  price: number | null = null;
  filteredSymbols: string[] = []; 
  allSymbols: string[] = []; 

  constructor(
    private orderService: OrderService,
    private signalRService: SignalRService 
  ) {}

  ngOnInit(): void {
    this.signalRService.companies$.subscribe((companies: CompanyWithHistoryGetDTO[]) => {
      if (companies) {
        this.allSymbols = companies.map((company: CompanyWithHistoryGetDTO) => company.company.symbol);
        this.filteredSymbols = [...this.allSymbols]; 
      }
    });
  }

  onSymbolInput(): void {
    if (this.stockSymbol.length > 0) {
      this.filteredSymbols = this.allSymbols.filter(symbol =>
        symbol.toLowerCase().includes(this.stockSymbol.toLowerCase())
      );
    } else {
      this.filteredSymbols = [];
    }
  }


  selectSymbol(symbol: string): void {
    this.stockSymbol = symbol;
    this.filteredSymbols = []; 

    this.signalRService.companies$.subscribe((companies: CompanyWithHistoryGetDTO[]) => {
      const company = companies.find((c: CompanyWithHistoryGetDTO) => c.company.symbol === symbol);
      if (company) {
        this.price = company.history[company.history.length - 1].price;
      }
    });
  }

  placeBuyOrder(): void {
    if (this.stockSymbol && this.quantity && this.price) {
      const numberOfShares = this.quantity / this.price;
      const orderData = {
        stockSymbol: this.stockSymbol,
        quantity: numberOfShares,
        price: this.price,
        type: 0
      };

      this.orderService.placeOrder(orderData).subscribe(
        response => {
          console.log(orderData);
          console.log('Buy Order placed successfully', response);
        },
        error => {
          console.log(orderData);
          console.error('Error placing Buy Order', error);
        }
      );
    }
  }

  placeSellOrder(): void {
    if (this.stockSymbol && this.quantity && this.price) {
      const numberOfShares = this.quantity / this.price;
      const orderData = {
        stockSymbol: this.stockSymbol,
        quantity: numberOfShares,
        price: this.price,
        type: 1 
      };

      this.orderService.placeOrder(orderData).subscribe(
        response => {
          console.log('Sell Order placed successfully', response);
        },
        error => {
          console.error('Error placing Sell Order', error);
        }
      );
    }
  }
}
