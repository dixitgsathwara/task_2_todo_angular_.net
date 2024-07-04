import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.username, this.password)
      .subscribe(
        (res: any) => {
          if (res.result == 1) {
            alert("Login successful");
            localStorage.setItem('token', res.token);
            this.authService.getUserInfo().subscribe(
              (userInfo: any) => {
                
                localStorage.setItem('username', userInfo.username);
                this.router.navigate(['/home']);
              },
              error => {
                console.error('Error fetching user info:', error);
                this.router.navigate(['/home']); 
              }
            );
          } else {
            alert("Invalid username or password");
          }
        },
        error => {
          console.error('Login error:', error);
          this.errorMessage = 'An error occurred during login. Please try again.';
        }
      );
  }
}
