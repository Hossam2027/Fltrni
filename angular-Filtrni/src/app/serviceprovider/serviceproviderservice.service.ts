import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../enviroment';
import { Iprovider } from '../Interfaces/Iprovider';
import { ICreateProvider } from '../Interfaces/IPostProvider';
import { HttpParams } from '@angular/common/http';
import { ICategory } from '../Interfaces/ICategory';
import { Pagination } from '../Interfaces/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ServiceproviderserviceService {
  apiurl = `${environment.apiurl}/serviceproviders`;
  // apiurlreviews = 'https://fltrni.runasp.net/api';
  apiurlreviews = `${environment.apiurl}`;

  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get<ICategory[]>(`${this.apiurlreviews}/Category`);
  }
  getServiceProviders(
    search: string = '',
    governorate: string = '',
    area: string = '',
    categoryId: number = 0,
    pageIndex: number = 1,
    pageSize: number = 12
  ) {
    let param = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());

    if (search) param = param.set('search', search);
    if (governorate) param = param.set('governorate', governorate);
    if (area) param = param.set('area', area);
    if (categoryId) {
      param = param.set('categoryId', categoryId.toString());
    }

    return this.http.get<Pagination<Iprovider>>(
      this.apiurl,
      { params: param }
    );
  }
  createServiceProvider(provider: ICreateProvider) {
    return this.http.post<ICreateProvider>(this.apiurl, provider);
  }
  getServiceProviderById(id: number) {
    const url = `${this.apiurl}/${id}`;
    return this.http.get<Iprovider>(url);
  }
  getServiceProvidersReviews(id: number) {
    return this.http.get<any[]>(`${this.apiurlreviews}/Review/provider/${id}`);
  }
  createServiceProviderReview(id: number, review: any) {
    return this.http.post<any>(`${this.apiurlreviews}/Review/provider/${id}`, review);

  }
  getAreas(categoryId: number = 0, governorate: string = '') {
    let params = new HttpParams();

    if (categoryId > 0) {
      params = params.set('categoryId', categoryId);
    }

    if (governorate) {
      params = params.set('governorate', governorate);
    }

    return this.http.get<string[]>(`${this.apiurl}/areas`, { params });
  }
}


