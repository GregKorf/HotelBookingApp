import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { CustomerService } from '../services/customer.service';

export const authGuard: CanActivateFn = (route, state) => {
  const customerService = inject(CustomerService);
  const router = inject(Router);
  console.log(customerService.customer()?.role);
  if (
    customerService.customer() &&
    customerService.customer()?.role === 'Customer'
  ) {
    return true;
  }

  return router.navigate(['/login']);
};
