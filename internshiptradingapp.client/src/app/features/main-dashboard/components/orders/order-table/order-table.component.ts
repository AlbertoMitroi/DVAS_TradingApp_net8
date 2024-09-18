import { Component, OnInit, inject } from '@angular/core';
import { SignalRService } from '../../../../../_services/signal-r.service';

interface Order {
  id: number;
  customerId: number;
  stockSymbol: string;
  quantity: number;
  price: number;
  type: number; 
  status: number;
  orderDate: Date;
}

@Component({
  selector: 'app-orders-table',
  templateUrl: './orders-table.component.html',
  styleUrls: ['./orders-table.component.css']
})
export class OrdersTableComponent implements OnInit {
  signalRService = inject(SignalRService);
  orders: Order[] = [];
  selectedTab: string = 'all';
  pAll: number = 1;
  pBuy: number = 1;
  pSell: number = 1;
  pageSize: number = 5;
  maxSize: number = 5;
  showActiveOnly: boolean = false;

  filteredOrders: Order[] = [];

  private typeMap: { [key: number]: string } = {
    0: 'Buy',
    1: 'Sell'
  };

  private statusMap: { [key: number]: string } = {
    0: 'Pending',
    1: 'Processing',
    2: 'Completed',
    3: 'Canceled',
    4: 'Failed'
  };

  ngOnInit(): void {
    this.signalRService.orders$.subscribe((orders: Order[]) => {
      this.orders = orders.reverse();
      console.log(orders);
      //this.orders.sort((a, b) => a.orderDate.getTime() - b.orderDate.getTime());
      this.filterOrders();
    });
  }

  filterOrders(): void {
    let filtered = this.orders;

    if (this.selectedTab === 'buy') {
      filtered = filtered.filter(order => this.typeMap[order.type] === 'Buy');
    } else if (this.selectedTab === 'sell') {
      filtered = filtered.filter(order => this.typeMap[order.type] === 'Sell');
    }

    if (this.showActiveOnly) {
      filtered = filtered.filter(order => order.status === 0 || order.status === 1);
    }

    this.filteredOrders = filtered;
  }

  selectTab(tab: string): void {
    this.selectedTab = tab;
    this.filterOrders();
    if (tab === 'all') this.pAll = 1;
    if (tab === 'buy') this.pBuy = 1;
    if (tab === 'sell') this.pSell = 1;
  }

  toggleActiveFilter(): void {
    this.showActiveOnly = !this.showActiveOnly;
    this.filterOrders();
  }

  getStatusClass(status: number): string {
    const statusString = this.statusMap[status];
    switch (statusString) {
      case 'Completed': return 'status-completed';
      case 'Pending': return 'status-pending';
      case 'Canceled':
      case 'Failed': return 'status-canceled';
      default: return '';
    }
    }
  

  getStatusText(status: number): string {
    const statusString = this.statusMap[status];
    switch (statusString) {
      case 'Completed': return 'Completed';
      case 'Pending': return 'Pending';
      case 'Canceled':
      case 'Failed': return 'Failed';
      case 'Processing': return 'Processing';
      default: return '';
    }
    }

  getOrderType(type: number): string {
    return this.typeMap[type] || '';
  }
}
