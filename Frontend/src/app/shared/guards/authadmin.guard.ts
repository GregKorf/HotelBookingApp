import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AdminService } from '../services/admin.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const adminService = inject(AdminService);
  const router = inject(Router);
  console.log(adminService.admin()?.role);
  if (adminService.admin() && adminService.admin()?.role === 'Admin') {
    return true;
  }

  return router.navigate(['/admin/login']);
};
