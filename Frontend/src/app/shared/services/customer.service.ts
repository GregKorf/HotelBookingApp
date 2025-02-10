import { Injectable, effect, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import {
  Credentials,
  JWTClaims,
  LoggedInUserCustomer,
} from '../interfaces/login.interface';
import { CustomerSignUp } from '../interfaces/customer.interface';
import { UserReadOnly } from '../interfaces/user-readonly.interface';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Booking } from '../interfaces/booking.interface';

const API_URL = `${environment.apiURL}`;

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  http: HttpClient = inject(HttpClient);
  router = inject(Router);

  customer = signal<LoggedInUserCustomer | null>(null);

  constructor() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedTokenSubject: JWTClaims = jwtDecode(token);

      if (
        decodedTokenSubject[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ] === 'Customer'
      ) {
        this.customer.set({
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
      }
    }
    effect(() => {
      if (this.customer()) {
        console.log('Customer logged in', this.customer()?.username);
      } else {
        console.log('No user logged in');
      }
    });
  }
  registerCustomer(customer: CustomerSignUp) {
    return this.http.post<UserReadOnly>(
      `${API_URL}/api/Customer/SignUpCustomer`,
      customer,
    );
  }

  loginCustomer(credentials: Credentials) {
    return this.http.post<{ token: string }>(
      `${API_URL}/api/Customer/LoginUser`,
      credentials,
    );
  }

  logoutCustomer() {
    this.customer.set(null);
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

  bookingHistoryCustomer() {
    return this.http.get<Booking[]>(
      `${API_URL}/api/Customer/GetCustomerBookings`,
    );
  }
}
