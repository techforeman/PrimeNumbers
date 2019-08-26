import { Component, OnInit } from '@angular/core';
import { ApiService } from 'app/api/api.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { SignalRService } from 'app/services/signal-r.service';




@Component({
  selector: 'app-basicelements',
  templateUrl: './basicelements.component.html',
  styleUrls: ['./basicelements.component.scss']
})
export class BasicelementsComponent implements OnInit {
  controlerName = 'results';
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
  all_xml_results: {};
  public async: any;


  constructor(private apiService: ApiService, private modalService: NgbModal, public signalRService: SignalRService) { }

  ngOnInit() {
    this.minRange = this.doubleSlider[0];
    this.maxRange = this.doubleSlider[1];

    this.signalRService.startConnection();
    this.signalRService.addProgressBarDataListener();
    this.apiService.startHttpRequest();

  }



  valueChange(value) {
    this.minRange = value[0];
    this.maxRange = value[1];

    console.log(this.maxRange + '   ' + this.minRange);
  }

  SearchPrimeNumber(content) {
    this.signalRService.sendMessageToHub();
    this.currentNumber = 0;
    this.isCalculating = true;
    this.openVerticallyCentered(content);
    this.messageTitle = 'Proszę czekać...';
    this.messageBody = 'Przeszukuję podany zakres liczb';
    this.apiService.addResults(this.minRange, this.maxRange, this.username, this.controlerName).subscribe(
      responseData => {
        console.log(responseData);
        this.messageTitle = "Gratulacje!";
        this.messageBody = 'Wyniki zapisano do bazy. Znalezione liczby to: ' + '\n' + '\n' + responseData['resultValues'];

        this.currentNumber = 100;
        this.isCalculating = false
        if (this.controlerName === 'resultsxml') {
          this.all_sql_results = this.apiService.getAllXmlResults();
        } else {
          this.all_sql_results = this.apiService.getAllResults();
        }
      },
      serverResponse => {
        ;

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

  CancelCalculation() {
    if (this.isCalculating == true) {
      if (this.controlerName === 'resultsxml') {
        this.all_sql_results = this.apiService.getAllXmlResults();
      } else {
        this.all_sql_results = this.apiService.getAllResults();
      }
      this.messageBody = "Dane nie zostały zapisane w bazie danych."
      this.messageTitle = "Operacja anulowana"
      setTimeout(() => {
        this.modalService.dismissAll();
      }, 5000);
    } else {
      this.modalService.dismissAll();
    }
  }
}
