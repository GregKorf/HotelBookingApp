import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LandingPageComponent } from "./features/customer/landing-page/landing-page.component";
import { NavbarComponent } from './shared/components/navbar/navbar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'hotel-booking-app';
}
