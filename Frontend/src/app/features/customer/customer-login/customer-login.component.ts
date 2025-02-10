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
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import {
  Credentials,
  JWTClaims,
} from '../../../shared/interfaces/login.interface';
import { CustomerService } from '../../../shared/services/customer.service';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-customer-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    NavbarComponent,
  ],
  templateUrl: './customer-login.component.html',
  styleUrl: './customer-login.component.css',
})
export class CustomerLoginComponent {
  customerService = inject(CustomerService);
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
    this.customerService.loginCustomer(credentials).subscribe({
      next: (response) => {
        const jwt_token = response.token;
        console.log(jwt_token);
        localStorage.setItem('token', jwt_token);

        const decodedTokenSubject: JWTClaims = jwtDecode(jwt_token);
        console.log(decodedTokenSubject);
        this.customerService.customer.set({
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

        this.router.navigate(['']);
      },
      error: (message) => {
        // console.log('Login Error', message);
        this.invalidLogin = true;
      },
    });
  }
}
