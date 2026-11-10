import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProviderservicesService } from './providerservices.service';

@Component({
  selector: 'app-providerstatus',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './providerstatus.component.html',
  styleUrl: './providerstatus.component.scss'
})
export class ProviderstatusComponent implements OnInit {
  myStatus: any = null;
  loading = true;
    errorMessage = '';

  constructor(private providerstatus: ProviderservicesService) { }
  ngOnInit() {
    this.getstatus();
  }

  getstatus() {
    this.providerstatus.getProviderStatus().subscribe({
      next: (response) => {
        this.myStatus = response;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching provider status:', error);
        this.loading = false;
      }
   } );  
  }


}
