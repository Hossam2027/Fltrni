import { Injectable } from '@angular/core';
import { environment } from '../../enviroment';
import { HttpClient } from '@angular/common/http';
import { Iprovider } from '../Interfaces/Iprovider';
import { PendingProvidersResponse } from '../Interfaces/PendingProvidersResponse ';



@Injectable({
  providedIn: 'root'
})
export class AdminserviceService {

  apiurl = `${environment.apiurl}/ServiceProviders`;

  constructor(private http: HttpClient) { }

  // pending  
  getpendingServiceProviders() {
    return this.http.get<PendingProvidersResponse>(`${this.apiurl}/pending`);
  }
// approved
  approveServiceProvider(id: number) 
  { return this.http.put(`${this.apiurl}/approve/${id}`,{},{ responseType: 'text' }
  );
  }

  rejectServiceProvider(id: number) 
  { return this.http.put(`${this.apiurl}/reject/${id}`,{},{ responseType: 'text' }
    );
  }
}
