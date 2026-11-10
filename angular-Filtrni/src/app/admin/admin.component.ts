import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminserviceService } from './adminservice.service';
import { Iprovider } from '../Interfaces/Iprovider';
@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {

  constructor(private adminService: AdminserviceService) { }

  pendingProviders: Iprovider[] = [];
  message = '';
  processingId: number | null = null;
  ngOnInit(): void {
    this.getPendingProviders();
  }

  getPendingProviders(): void {
    this.adminService.getpendingServiceProviders().subscribe({
      next: (response) => {
        this.pendingProviders = response.data;

        console.log('Pending providers:', this.pendingProviders);
      },
      error: (error) => {
        console.error('Error loading pending providers:', error);
      }
    });
  }

  approveProvider(id: number): void {
    this.processingId = id;
    this.message = '';

    this.adminService.approveServiceProvider(id).subscribe({
      next: () => {
        this.message = 'Service provider approved successfully.';
        this.processingId = null;
        this.getPendingProviders();
      },

      error: (error) => {
        console.error(`Error approving provider ${id}:`, error);
        this.processingId = null;
      }
    });
  }

  rejectProvider(id: number): void {
    this.processingId = id;
    this.message = '';
    this.adminService.rejectServiceProvider(id).subscribe({
      next: () => {
        this.message = 'Service provider rejected successfully.';
        this.processingId = null;
        this.getPendingProviders();
      },
      error: (error) => {
        console.error(`Error rejecting provider ${id}:`, error);
        this.processingId = null;
      }
    });
  }
}
