import { Injectable, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';
import { UserDetailsDto } from '../_models/userDetailsDto';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnDestroy {
  private hubUrl = 'https://localhost:7221/hubs/';
  private hubConnection?: HubConnection;
  private userDetailsSubject = new BehaviorSubject<UserDetailsDto | null>(null);
  userDetails$ = this.userDetailsSubject.asObservable();

  constructor(private toastr: ToastrService) {}

  createHubConnection(user: User): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.hubUrl}userHub`, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: retryContext => {
          if (retryContext.previousRetryCount < 5) {
            return 1000 * Math.pow(2, retryContext.previousRetryCount);
          }
          return null;
        }
      })
      .build();

    this.startConnection();
    this.registerEvents();
  }
  createOrderHubConnection(user: User): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.hubUrl}orderHub`, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: retryContext => {
          if (retryContext.previousRetryCount < 5) {
            return 1000 * Math.pow(2, retryContext.previousRetryCount);
          }
          return null;
        }
      })
      .build();

    this.startOrderConnection();
    this.registerOrderEvents();
  }

  private async startConnection(): Promise<void> {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      return;
    }

    try {
      await this.hubConnection?.start();
      console.log('SignalR Connected');
      await this.hubConnection?.invoke('SendData');
    } catch (error) {
      console.error('SignalR Connection Error:', error);
    }
  }

  private async startOrderConnection(): Promise<void> {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      return;
    }

    try {
      await this.hubConnection?.start();
      console.log('SignalR Connected');
      await this.hubConnection?.invoke('SendOrderUpdate');
    } catch (error) {
      console.error('SignalR Connection Error:', error);
    }
  }

  private registerEvents(): void {
    this.hubConnection?.on('ReceiveUserDetails', (userDetails: UserDetailsDto) => {
      this.toastr.info(`${userDetails.username} has connected`);
      this.userDetailsSubject.next(userDetails);
    });
  }

  private registerOrderEvents(): void {
    this.hubConnection?.on('ReceiveOrders', (orders: any) => {
      this.userDetailsSubject.next(orders);
    });
  }

  stopHubConnection(): void {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      this.hubConnection.stop().catch(error => console.error('SignalR Disconnection Error:', error));
    }
  }

  ngOnDestroy(): void {
    this.stopHubConnection();
  }
}
