import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;
  private progressSubject: Subject<string> = new Subject<string>();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44429/progresshub')
      .build();

    this.hubConnection.on('ReceiveProgressUpdate', (progressMessage: string) => {
      this.progressSubject.next(progressMessage);
    });

    this.hubConnection.start().catch(err => console.error(err));
  }

  getProgressUpdates() {
    return this.progressSubject.asObservable();
  }
}
