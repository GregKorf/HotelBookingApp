import { Injectable, effect, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import {
  Credentials,
  JWTClaims,
  LoggedInUserAdmin,
} from '../interfaces/login.interface';
import { AdminSignup } from '../interfaces/admin.interface';
import { UserReadOnly } from '../interfaces/user-readonly.interface';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AllBooking } from '../interfaces/booking.interface';

const API_URL = `${environment.apiURL}`;

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  http: HttpClient = inject(HttpClient);
  router = inject(Router);

  admin = signal<LoggedInUserAdmin | null>(null);

  constructor() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedTokenSubject: JWTClaims = jwtDecode(token);

      if (
        decodedTokenSubject[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ] === 'Admin'
      ) {
        this.admin.set({
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
      if (this.admin()) {
        console.log('Admin logged in', this.admin()?.username);
      } else {
        console.log('No user logged in');
      }
    });
  }

  registerAdmin(admin: AdminSignup) {
    return this.http.post<UserReadOnly>(
      `${API_URL}/api/Admin/SignUpAdmin`,
      admin,
    );
  }

  loginAdmin(credentials: Credentials) {
    return this.http.post<{ token: string }>(
      `${API_URL}/api/Admin/LoginUser`,
      credentials,
    );
  }

  logoutAdmin() {
    this.admin.set(null);
    localStorage.removeItem('token');
    this.router.navigate(['/admin/login']);
  }

  bookingsHistory() {
    return this.http.get<AllBooking[]>(`${API_URL}/api/Admin/GetAllBookings`);
  }
}
