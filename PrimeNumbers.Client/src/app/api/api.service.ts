import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiURL = 'http://localhost:5000/api/';
  controlerName = 'results/';
  controlerXmlName = 'resultsxml/';
  messageTitle = '';
  messageBody = '';

  constructor(private httpClient: HttpClient) { }


  public async getAllResults() {
    return await this.httpClient.get(this.apiURL + this.controlerName).toPromise();
  }

  public async getAllXmlResults() {
    return await this.httpClient.get(this.apiURL + this.controlerXmlName).toPromise();
  }


  public deleteResult(id: number) {
    return this.httpClient.delete(this.apiURL + this.controlerName + id + '/');
  }

  public addResults(minRange: number, maxRange: number, username: string, controlerName: string) {
    return this.httpClient.post(this.apiURL + controlerName + '?minRange=' + minRange + '&maxRange=' + maxRange + '&username=' + username, '');
  }


  public startHttpRequest = () => {
    return this.httpClient.get('https://localhost:5000/progresstask')
      .subscribe(res => {
        console.log(res);
      })
  }



  alertMessage(status) {
    if (status === 0) {
      console.log('Nie można wpisać wartości do bazy danych');
      this.messageTitle = 'Serwer odmówił posłuszeństwa!';
      this.messageBody = 'Skontaktuj się z adminem.';
    } else if (status === '401') {
      console.log('Nie masz uprawnień');
      this.messageTitle = 'Błędne dane logowania !';
      this.messageBody = 'Nie posiadasz dostępu / uprawnień';
    } else if (status === '400') {
      console.log('Błędne dane');
      this.messageTitle = 'Błędne dane !';
      this.messageBody = 'Proszę popraw dane';
    } else if (status === '403') {
      console.log('Błędne dane');
      this.messageTitle = 'Błędne dane!';
      this.messageBody = 'Proszę popraw dane';
    } else if (status === '500') {
      console.log('Serwer nie odpowiada');
      this.messageTitle = 'Serwer nie odpowiada !';
      this.messageBody = 'Prawdopodobnie chcesz wpisać nie poprawne dane lub serwer nie odpwiada,';
    } else {
      console.log('Wszystko ok');
      this.messageTitle = 'Gratulacje !';
      this.messageBody = 'Dane przeszukane prawidłowo.';
    }
    return { messageTitle: this.messageTitle, messageBody: this.messageBody };
  }
}


