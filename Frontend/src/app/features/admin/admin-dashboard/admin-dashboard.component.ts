import { Component, inject } from '@angular/core';
import { AdminNavbarMenuComponent } from '../../../shared/components/admin-navbar-menu/admin-navbar-menu.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [AdminNavbarMenuComponent],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css',
})
export class AdminDashboardComponent {}
