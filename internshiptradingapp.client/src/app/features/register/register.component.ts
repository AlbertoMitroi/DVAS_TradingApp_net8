import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../_services/auth.service'; 
import { RegisterDto } from '../../_models/RegisterDto';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.registerForm = this.fb.group({
      fullname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordsMatch });
  }

  get fullname() {
    return this.registerForm.get('fullname');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  passwordsMatch(group: FormGroup): ValidationErrors | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log('Form Values:', this.registerForm.value);

      const registerDto : RegisterDto = {
        fullname: this.registerForm.value.fullname,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      };


      console.log('Payload:', registerDto);

      this.authService.register(registerDto).subscribe({
        next: user => {

          console.log('Registration Successful:', user);


          console.log('Full Response:', user);


          window.location.href = '/';
          /*this.router.navigate(['/']);*/
        },
        error: error => {

          console.error('Registration Error:', error);


          console.error('Full Error Object:', error);


          if (error.error && typeof error.error === 'string') {
            this.toastr.error(error.error);
          } else {
            this.toastr.error('An error occurred');
          }
        }
      });
    } else {

      console.log('Form is invalid:', this.registerForm.errors);
    }
  }
}
