import { Injectable, OnInit } from '@angular/core';
import { ProviderstatusComponent } from './providerstatus.component';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProviderservicesService  {


  // apiurl = 'https://fltrni.runasp.net/api';
  apiurl = 'https://localhost:44396/api';
  constructor(private http: HttpClient) { }

  getProviderStatus() {
    return this.http.get<any>(`${this.apiurl}/ServiceProviders/my-status`);
  }
}
