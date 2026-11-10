import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [[FormsModule, ReactiveFormsModule, CommonModule, CommonModule,
    ReactiveFormsModule,
    RouterModule]],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService
  ) { }
  ngOnInit(): void {
  }


  loginform = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });
  login() {
    if (this.loginform.valid) {
      this.authService.login(this.loginform.value).subscribe(
        (response: any) => {

          this.authService.saveToken(response.token);
          this.authService.saveUserName(response.userName);
          console.log('User Role:', this.authService.getUserRole());
          console.log('Login successful:', response);
          this.toastr.success(
            'تم تسجيل الدخول بنجاح',
            'تم بنجاح'
          );
          this.router.navigate(['/serviceprovider']);
        },
        (error: any) => {
          console.error('Login failed:', error);
          this.toastr.error(
            'فشل تسجيل الدخول',
            'حدث خطأ'
          );
        }
      );
    }
  }

}
