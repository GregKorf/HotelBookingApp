import { Routes } from '@angular/router';
import { LandingPageComponent } from './features/customer/landing-page/landing-page.component';
import { CustomerLoginComponent } from './features/customer/customer-login/customer-login.component';
import { CustomerSignupComponent } from './features/customer/customer-signup/customer-signup.component';
import { BookingHistoryComponent } from './features/customer/booking-history/booking-history.component';
import { RoomBookingComponent } from './features/customer/room-booking/room-booking.component';
import { ManageBookingsComponent } from './features/admin/manage-bookings/manage-bookings.component';
import { AdminDashboardComponent } from './features/admin/admin-dashboard/admin-dashboard.component';
import { AdminLoginComponent } from './features/admin/admin-login/admin-login.component';
import { ManageRoomsComponent } from './features/admin/manage-rooms/manage-rooms.component';
import { authGuard } from './shared/guards/auth.guard';
import { adminGuard } from './shared/guards/authadmin.guard';
import { AdminSignupComponent } from './features/admin/admin-signup/admin-signup.component';

export const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'login', component: CustomerLoginComponent },
  { path: 'signup', component: CustomerSignupComponent },
  {
    path: 'booking-history',
    component: BookingHistoryComponent,
    canActivate: [authGuard],
  },
  {
    path: 'room-booking',
    component: RoomBookingComponent,
    canActivate: [authGuard],
  },

  // Admin routes
  { path: 'admin/login', component: AdminLoginComponent },
  { path: 'admin/register', component: AdminSignupComponent },
  {
    path: 'admin/dashboard',
    component: AdminDashboardComponent,
    canActivate: [adminGuard],
  },
  {
    path: 'admin/manage-rooms',
    component: ManageRoomsComponent,
    canActivate: [adminGuard],
  },
  {
    path: 'admin/manage-bookings',
    component: ManageBookingsComponent,
    canActivate: [adminGuard],
  },
];
