import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../signal-r.service';

@Component({
  selector: 'app-signal-r',
  templateUrl: './signal-r.component.html'
})
export class SignalRComponent implements OnInit {
  progressUpdates: string[] = [];
  _httpClient: HttpClient;
  

  constructor(private httpClient: HttpClient, private signalRService: SignalRService) {
    this._httpClient = httpClient;
  }

  ngOnInit() {
    this.signalRService.getProgressUpdates().subscribe((progressMessage: string) => {
      this.progressUpdates.push(progressMessage);
    });
  }

  startProcess() {
    // Call the API endpoint that starts the process on the server
    // For example, using HttpClient
    // Make sure to replace 'your-api-url' with the actual URL of your API
    this.httpClient.post('https://localhost:44429/weatherforecast/startProcess', {}).subscribe();
  }
}
