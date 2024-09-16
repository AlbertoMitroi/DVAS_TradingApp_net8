import { Component } from '@angular/core';

interface Order {
  id: number;
  customerId: number;
  stockSymbol: string;
  quantity: number;
  price: number;
  type: 'Buy' | 'Sell';
  status: 'Pending' | 'Completed' | 'Canceled' | 'Failed';
  orderDate: Date;
}

@Component({
  selector: 'app-orders-table',
  templateUrl: './orders-table.component.html',
  styleUrls: ['./orders-table.component.css']
})
export class OrdersTableComponent {
  orders: Order[] = [
    {
      id: 1,
      customerId: 101,
      stockSymbol: 'AAPL',
      quantity: 50,
      price: 150.25,
      type: 'Buy',
      status: 'Completed',
      orderDate: new Date('2024-09-13')
    },
    {
      id: 2,
      customerId: 102,
      stockSymbol: 'GOOGL',
      quantity: 10,
      price: 2800.50,
      type: 'Sell',
      status: 'Pending',
      orderDate: new Date('2024-09-14')
    },
    {
      id: 3,
      customerId: 103,
      stockSymbol: 'MSFT',
      quantity: 25,
      price: 300.75,
      type: 'Buy',
      status: 'Failed',
      orderDate: new Date('2024-09-10')
    },
    {
      id: 4,
      customerId: 104,
      stockSymbol: 'TSLA',
      quantity: 15,
      price: 1200.10,
      type: 'Sell',
      status: 'Canceled',
      orderDate: new Date('2024-09-11')
    }
  ];

  getStatusClass(status: string): string {
    switch (status) {
      case 'Completed':
        return 'status-completed';
      case 'Pending':
        return 'status-pending';
      case 'Canceled':
      case 'Failed':
        return 'status-canceled';
      default:
        return '';
    }
  }
}
