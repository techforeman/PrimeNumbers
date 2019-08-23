import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public progressValue: 0;
  private hubConnection: signalR.HubConnection

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                          .withUrl('http://localhost:5000/api/progress')
                          .build();


    this.hubConnection
      .start()
      .then(() => console.log('Connection start'))
      .catch(err => console.log('Error while starting connection: + err'))

      
  }


  constructor() { }
}
