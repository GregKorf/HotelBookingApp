import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { Router } from '@angular/router';
import { CarouselModule } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [CommonModule, NavbarComponent, CarouselModule],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {
  router = inject(Router);

  images = [
    '/images/pool.jpg',
    '/images/hotel.jpg',
    '/images/suiteroom.jpg'
  ];

  carouselOptions = {
    loop: true,
    dots: true,
    nav: true,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    items: 1, // Ensures only one image is shown at a time
    responsive: {
      0: { items: 1 },   // 1 item on small screens
      600: { items: 1 }, // 1 item on medium screens
      1000: { items: 1 } // 1 item on large screens
    }
  }
}
