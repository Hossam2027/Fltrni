import { Component, OnInit } from '@angular/core';
import { ServiceproviderserviceService } from '../serviceprovider/serviceproviderservice.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Iprovider } from '../Interfaces/Iprovider';
import { Ireview } from '../Interfaces/Ireview';
import { FormsModule } from '@angular/forms';
import { ICreatereview } from '../Interfaces/iCreatereview';
import { TranslateModule } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-providerdetails',
  standalone: true,
  imports: [CommonModule , FormsModule, TranslateModule],
  templateUrl: './providerdetails.component.html',
  styleUrl: './providerdetails.component.scss'
})
export class ProviderdetailsComponent implements OnInit {
  provider: Iprovider | null = null;
  submitted = false;
  reviews: Ireview[] = [];
    review = {
    rating: 5,
    comment: ''
  };
  ngOnInit(): void {
    this.getallServiceProvidersbyid();
    this.getallServiceProvidersReviews();
  }
  constructor(private serviceProviderService: ServiceproviderserviceService, private route: ActivatedRoute , private toastr:ToastrService) { }
  getallServiceProvidersbyid() {

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.serviceProviderService.getServiceProviderById(id).subscribe({
      next: (response) => {
        this.provider = response;
        console.log('Service Provider:', response);
      },
      error: (error) => {
        console.error('Error fetching service provider:', error);
      }}
    );
  
  }
  getallServiceProvidersReviews() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.serviceProviderService.getServiceProvidersReviews(id).subscribe({
      next: (response) => {
        this.reviews = response;
        console.log('Service Provider Reviews:', response);
     
      },
      error: (error) => {
        console.error('Error fetching service provider reviews:', error);
      }
    });
  }

  createReview( ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const reviewData = {serviceProviderId: id,rating: this.review.rating,comment: this.review.comment
    
    };
     
    this.serviceProviderService.createServiceProviderReview(id, reviewData).subscribe({
      next:   (response) => {
        console.log('Review created:', response);
        this.getallServiceProvidersReviews();
        this.getallServiceProvidersbyid();
          this.review = {
          rating: 5,
          comment: ''
        };
           this.toastr.success(
  '  تم اضافه تعليقك بنجاح    ', );
      },
      error: (error) => {
        console.error('Error creating review:', error);
        this.toastr.error('المستخد لم  يسجل تسجيل ذخول')
      }
   } );
  }
}
