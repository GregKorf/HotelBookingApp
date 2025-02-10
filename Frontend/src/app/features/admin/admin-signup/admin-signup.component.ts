import { Component, inject } from '@angular/core';
import { AdminNavbarMenuComponent } from '../../../shared/components/admin-navbar-menu/admin-navbar-menu.component';
import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { AdminService } from '../../../shared/services/admin.service';
import { AdminSignup } from '../../../shared/interfaces/admin.interface';
@Component({
  selector: 'app-admin-signup',
  imports: [
    AdminNavbarMenuComponent,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
  ],
  templateUrl: './admin-signup.component.html',
  styleUrl: './admin-signup.component.css',
})
export class AdminSignupComponent {
  adminService = inject(AdminService);

  registrationStatus: { success: boolean; message: string } = {
    success: false,
    message: 'Not attempted yet',
  };

  form = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Z0-9]+$'),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
    firstName: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Zα-ωΑ-Ω]+$'),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Zα-ωΑ-Ω]+$'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4),
      Validators.pattern(
        '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{4,}$',
      ),
    ]),
    phoneNumber: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
      Validators.pattern('^[0-9]+$'),
    ]),
    jobDescription: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Zα-ωΑ-Ω]+$'),
    ]),
  });

  onSubmit() {
    if (this.form.invalid) {
      this.registrationStatus = {
        success: false,
        message: 'Form is invalid. Please check your inputs.',
      };
      return;
    }

    const user: AdminSignup = {
      username: this.form.get('username')?.value || '',
      email: this.form.get('email')?.value || '',
      firstName: this.form.get('firstName')?.value || '',
      lastName: this.form.get('lastName')?.value || '',
      password: this.form.get('password')?.value || '',
      jobDescription: this.form.get('jobDescription')?.value || '',
      phoneNumber: this.form.get('phoneNumber')?.value || '',
    };

    this.adminService.registerAdmin(user).subscribe({
      next: (response) => {
        console.log('No Errors', response);
        this.registrationStatus = {
          success: true,
          message: `Admin ${response.username} registered successfully!`,
        };
        this.form.reset();
      },
      error: (response) => {
        console.log('Errors', response);
        const message =
          response.error.message || 'Registration failed. Please try again.';
        this.registrationStatus = { success: false, message: message };
      },
    });
  }
}
