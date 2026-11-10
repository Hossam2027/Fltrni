import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule ,  
  ReactiveFormsModule,
  RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  
  constructor(private authService: AuthService, private router: Router , private toastr:ToastrService) { }
  ngOnInit(): void {
  }
  registerform = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),

  });
  register() {
    if (this.registerform.valid) {
      this.authService.register(this.registerform.value).subscribe(
        (response: any) => {
          // Handle successful registration 
          console.log('Registration successful:', response);
          this.toastr.success(
  '  تسجيل طلبك بنجاح    ', );
          this.router.navigate(['/login']); 
        },
        (error: any) => {
          // Handle registration error
          console.error('Registration failed:', error);
        }
      );
    }
  }

}
