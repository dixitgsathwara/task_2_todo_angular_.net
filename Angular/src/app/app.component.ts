import { Component } from '@angular/core';
import { AuthService } from './auth.service'; // Adjust path as needed
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService, private router: Router) {}

  isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  getUsername(): string {
    return localStorage.getItem('username') || '';
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
