import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { AdminService } from '../../../shared/services/admin.service';

@Component({
  selector: 'app-admin-navbar-menu',
  standalone: true,
  imports: [MatIconModule, RouterModule],
  templateUrl: './admin-navbar-menu.component.html',
  styleUrl: './admin-navbar-menu.component.css',
})
export class AdminNavbarMenuComponent {
  adminService = inject(AdminService);
  admin = this.adminService.admin;

  logoutAdmin() {
    this.adminService.logoutAdmin();
  }
}
