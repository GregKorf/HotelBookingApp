import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule, MatIconModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  customerService = inject(CustomerService);
  customer = this.customerService.customer;

  logout() {
    this.customerService.logoutCustomer();
  }
}
