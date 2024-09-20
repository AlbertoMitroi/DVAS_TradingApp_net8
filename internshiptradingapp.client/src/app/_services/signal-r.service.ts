import { Injectable, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnDestroy {
  private hubUrl = 'https://localhost:7221/hubs/';
  private hubConnections: { [hubName: string]: HubConnection } = {};
  private dataSubjects: { [hubName: string]: BehaviorSubject<any> } = {};
  private isInitialized: { [hubName: string]: boolean } = {};

  userDetails$ = new BehaviorSubject<any>(null).asObservable();
  orders$ = new BehaviorSubject<any>(null).asObservable();
  companies$ = new BehaviorSubject<any>(null).asObservable();
  
  constructor() {
    this.dataSubjects['userHub'] = new BehaviorSubject<any>(null);
    this.dataSubjects['orderHub'] = new BehaviorSubject<any>(null);
    this.dataSubjects['companiesHub'] = new BehaviorSubject<any>(null);
  

    this.userDetails$ = this.dataSubjects['userHub'].asObservable();
    this.orders$ = this.dataSubjects['orderHub'].asObservable();
    this.companies$ = this.dataSubjects['companiesHub'].asObservable();
  }

  initializeSignalRConnections(user: User): void {
    if (this.isAlreadyConnected()) {
      console.log('SignalR connections already initialized.');
      return;
    }
    this.createHubConnection(user, 'userHub', 'SendData', 'ReceiveUserDetails');
    this.createHubConnection(user, 'orderHub', 'SendOrderUpdate', 'ReceiveOrders');
    this.createHubConnection(user, 'companiesHub', 'UpdateCompaniesData', 'ReceiveCompanies');
    console.log('Initialize SignalR Connections');
  }

  private isAlreadyConnected(): boolean {
    return Object.keys(this.hubConnections).some(hubName => {
      const connection = this.hubConnections[hubName];
      return connection?.state === HubConnectionState.Connected;
    });
  }


  private createHubConnection(user: User, hubName: string, invokeMethod: string, receiveEvent: string): void {
    if (this.isInitialized[hubName]) return;

    const hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.hubUrl}${hubName}`, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnections[hubName] = hubConnection;
    this.isInitialized[hubName] = true;

    this.startConnection(hubName, invokeMethod);
    this.registerEvents(hubName, receiveEvent);
  }

  private async startConnection(hubName: string, invokeMethod: string): Promise<void> {
    const hubConnection = this.hubConnections[hubName];
    if (hubConnection.state === HubConnectionState.Connected) {
      return;
    }

    try {
      await hubConnection?.start();
      console.log('SignalR Connected');
      await hubConnection?.invoke(invokeMethod);
    } catch (error) {
      console.error('SignalR Connection Error:', error);
    }
  }

  private registerEvents(hubName: string, event: string): void {
    const hubConnection = this.hubConnections[hubName];
    if (hubConnection) {
      hubConnection.on(event, (data : any) => {
        //console.log(`On ${event} with hub ${hubName} with response ${data}`)
        //console.log(`${event} received from ${hubName}:`, data);
        this.dataSubjects[hubName].next(data);
      });
    }
  }

  stopHubConnection(hubName: string): void {
    const hubConnection = this.hubConnections[hubName];
    hubConnection?.stop()
      .then(() => {
        console.log(`Disconnected from ${hubName}`);
      })
      .catch(err => console.error('Error stopping connection:', err));
  }

  stopAllHubConnections(): void {
    Object.keys(this.hubConnections).forEach(hubName => this.stopHubConnection(hubName));
  }

  ngOnDestroy(): void {
    this.stopAllHubConnections();
  }

  transformUsername(username: string): string {
    return username
      .split('_')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }
}
