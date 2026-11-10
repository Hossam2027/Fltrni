import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { environment } from '../../../enviroment';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiurl = `${environment.apiurl}/Auth`;
  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: object) { }
  private userNameSubject = new BehaviorSubject<string | null>(
    this.getUserName()
  );

  userName$ = this.userNameSubject.asObservable();
  

  saveUserName(userName: string): void {
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem('userName', userName);
    }
    this.userNameSubject.next(userName);
  }

  login(data: any) {
    return this.http.post<any>(`${this.apiurl}/login`, data);
  }

  register(data: any) {
    return this.http.post<any>(`${this.apiurl}/register`, data);
  }

  logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      localStorage.removeItem('userName');
    }
    this.userNameSubject.next(null);

  }
  saveToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('token', token);
    }

  }
  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('token');
    }

    return null;
  }
  getUserRole(): string | null {
    const token = this.getToken();

    if (!token) {
      return null;
    }

    try {
      const payload = token.split('.')[1];
      const decodedPayload = JSON.parse(atob(payload));

      const role =
        decodedPayload.role ??
        decodedPayload.roles ??
        decodedPayload[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];

      return Array.isArray(role) ? role[0] ?? null : role ?? null;
    } catch {
      return null;
    }
  }
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

 getUserName(): string | null {
    if (typeof localStorage === 'undefined') {
      return null;
    }
    return localStorage.getItem('userName');
  }
 

}
