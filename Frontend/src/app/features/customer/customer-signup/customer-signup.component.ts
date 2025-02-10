import { Component, inject } from '@angular/core';
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
import { CustomerSignUp } from '../../../shared/interfaces/customer.interface';
import { CustomerService } from '../../../shared/services/customer.service';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';

@Component({
  selector: 'app-customer-signup',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    NavbarComponent,
  ],
  templateUrl: './customer-signup.component.html',
  styleUrl: './customer-signup.component.css',
})
export class CustomerSignupComponent {
  userService = inject(CustomerService);

  registrationStatus: { success: boolean; message: string } = {
    success: false,
    message: 'Not attempted yet',
  };

  form = new FormGroup(
    {
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
      confirmPassword: new FormControl('', Validators.required),
      address: new FormControl('', [
        Validators.required,
        Validators.pattern('^[a-zA-Zα-ωΑ-Ω0-9 ]+$'),
      ]),
      city: new FormControl('', [
        Validators.required,
        Validators.pattern('^[a-zA-Zα-ωΑ-Ω]+$'),
      ]),
      phoneNumber: new FormControl('', [
        Validators.required,
        Validators.minLength(10),
        Validators.pattern('^[0-9]+$'),
      ]),
    },
    this.passwordConfirmPasswordValidator,
  );

  passwordConfirmPasswordValidator(
    control: AbstractControl,
  ): { [key: string]: boolean } | null {
    const form = control as FormGroup;
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;

    if (password && confirmPassword && password !== confirmPassword) {
      form.get('confirmPassword')?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }

    return null;
  }

  onSubmit() {
    if (this.form.invalid) {
      this.registrationStatus = {
        success: false,
        message: 'Form is invalid. Please check your inputs.',
      };
      return;
    }

    const user: CustomerSignUp = {
      username: this.form.get('username')?.value || '',
      email: this.form.get('email')?.value || '',
      firstName: this.form.get('firstName')?.value || '',
      lastName: this.form.get('lastName')?.value || '',
      password: this.form.get('password')?.value || '',
      address: this.form.get('address')?.value || '',
      city: this.form.get('city')?.value || '',
      phoneNumber: this.form.get('phoneNumber')?.value || '',
    };

    this.userService.registerCustomer(user).subscribe({
      next: (response) => {
        console.log('No Errors', response);
        this.registrationStatus = {
          success: true,
          message: `User ${response.username} registered successfully!`,
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
