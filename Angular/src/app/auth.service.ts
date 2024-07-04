import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5246/api'; 
  private tokenKey = 'token';

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, { username, password });
  }

  logout() {
    localStorage.removeItem(this.tokenKey); 
    this.router.navigate(['/login']); 
  }

  isAuthenticated(): boolean {
    
    const token = localStorage.getItem(this.tokenKey);

    return !!token;
  }

  getUserInfo() {
    const token = localStorage.getItem(this.tokenKey);
  
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<any>(`${this.apiUrl}/Values/admin`, { headers });
  }
}
