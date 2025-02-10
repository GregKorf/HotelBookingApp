import { Component, inject } from '@angular/core';
import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {
  Credentials,
  JWTClaims,
} from '../../../shared/interfaces/login.interface';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AdminService } from '../../../shared/services/admin.service';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
  ],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.css',
})
export class AdminLoginComponent {
  adminService = inject(AdminService);
  router = inject(Router);

  invalidLogin = false;

  form = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Z0-9]+$'),
    ]),
    password: new FormControl('', Validators.required),
  });

  onSubmit() {
    const credentials = this.form.value as Credentials;
    this.adminService.loginAdmin(credentials).subscribe({
      next: (response) => {
        const jwt_token = response.token;
        console.log(jwt_token);
        localStorage.setItem('token', jwt_token);

        const decodedTokenSubject: JWTClaims = jwtDecode(jwt_token);
        console.log(decodedTokenSubject);
        this.adminService.admin.set({
          id: decodedTokenSubject[
            'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
          ],
          username:
            decodedTokenSubject[
              'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
            ],
          email:
            decodedTokenSubject[
              'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
            ],
          role: decodedTokenSubject[
            'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
          ],
        });

        this.router.navigate(['/admin/dashboard']);
      },
      error: (message) => {
        // console.log('Login Error', message);
        this.invalidLogin = true;
      },
    });
  }
}
