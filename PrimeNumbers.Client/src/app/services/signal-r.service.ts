import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
public data: [];
private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:5000/progresstask')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
 
  public addProgressBarDataListener = () => {
    this.hubConnection.on('Send', (data) => {
      this.data = data;
      console.log(data);
    });
  }

  public sendMessageToHub = () => {
    this.hubConnection.send('Send', 'TestHuba')
    .catch(err => console.error(err));
  }
}
