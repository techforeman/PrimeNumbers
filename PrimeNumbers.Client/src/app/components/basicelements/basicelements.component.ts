import { Component, OnInit } from '@angular/core';
import { FormsModule, FormGroup, NgModel } from '@angular/forms';
import { ApiService } from 'app/api/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';


@Component({
  selector: 'app-basicelements',
  templateUrl: './basicelements.component.html',
  styleUrls: ['./basicelements.component.scss']
})
export class BasicelementsComponent implements OnInit {
  
  messageBody = '';
  messageTitle = '';
  username = 'Gall Anonim';
  serverResponse = '';
   minRange = 0;
   maxRange = 0;
    doubleSlider = [1, 19999];
    state = true;
    maxNumber = 100;
    currentNumber = 1;
    isCalculating = false;
    all_sql_results: {};

    private _hubConnection: HubConnection | undefined;
    public async: any;
    message = '';
    messages: string[] = [];

    constructor(private apiService: ApiService, private modalService: NgbModal) { }

    ngOnInit() {
      this.minRange = this.doubleSlider[0];
      this.maxRange = this.doubleSlider[1];

      this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5000/progress')
            .configureLogging(signalR.LogLevel.Information)
            .build();
 
        this._hubConnection.start().catch(err => console.error(err.toString()));
 
        this._hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });
      
    }

    public sendMessage(): void {
      const data = `Sent: ${this.message}`;

      if (this._hubConnection) {
          this._hubConnection.invoke('Send', data);
      }
      this.messages.push(data);
  }

    valueChange(value) {
      this.minRange = value[0];
      this.maxRange = value[1];

      console.log(this.maxRange + '   ' + this.minRange);
    }

    SearchPrimeNumber(content)
    {
      this.currentNumber = 0;
      this.isCalculating = true;
      this.openVerticallyCentered(content);
      this.messageTitle = 'Proszę czekać...';
      this.messageBody = 'Przeszukuję podany zakres liczb';
      this.apiService.addResults(this.minRange, this.maxRange, this.username).subscribe(
        responseData => {
          console.log(responseData);
          this.messageTitle = "Gratulacje!";
          this.messageBody = 'Wyniki zapisano do bazy';
          /* this.messageBody = responseData.valueOf['resultValues'].toString(); */
          this.currentNumber = 100;
          this.isCalculating = false
          this.all_sql_results = this.apiService.getAllResults();
        },
        serverResponse => {;
         
          this.serverResponse = serverResponse;
          console.log(serverResponse.status);
          this.alertMessage(serverResponse.status);
         
        }
      );
    }

    alertMessage(status) {
      this.messageBody = this.apiService.alertMessage(status).messageBody;
      this.messageTitle = this.apiService.alertMessage(status).messageTitle;
    
      }

      openVerticallyCentered(content) {
        this.modalService.open(content, { centered: true });
    }

    CancelCalculation()
    {
     if(this.isCalculating==true)
      {
       
       this.all_sql_results = this.apiService.getAllResults();
       this.messageBody = "Dane nie zostały zapisane w bazie danych."
       this.messageTitle = "Operacja anulowana"
       setTimeout(() => 
        {
          this.modalService.dismissAll(); 
        }, 5000);
      } else {
        this.modalService.dismissAll();
      }
}
}
