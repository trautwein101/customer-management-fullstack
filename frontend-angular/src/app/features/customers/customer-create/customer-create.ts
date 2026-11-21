import { Component, inject, signal } from '@angular/core';
import { form, FormField, required, email } from '@angular/forms/signals';
import { Router, RouterLink } from '@angular/router';
import { CustomerService } from '../services/customer';
import { finalize } from 'rxjs';
import { CreateCustomerRequest } from '../models/customer.model';

@Component({
  imports: [FormField, RouterLink],
  selector: 'app-customer-create',
  styleUrl: './customer-create.scss',
  templateUrl: './customer-create.html',
})
export class CustomerCreate {
  private readonly customerService = inject(CustomerService);
  private readonly router = inject(Router);

  customerModel = signal({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: ''
  });

  customerForm = form(this.customerModel, (path) => {
    required(path.firstName);
    required(path.lastName);
    required(path.email);
    email(path.email);
  });

  isLoading = signal(false);
  errorMessage = signal('');
  
  saveCustomer(): void {

    if(this.customerForm().invalid()) {
      this.customerForm.firstName().markAsTouched();
      this.customerForm.lastName().markAsTouched();
      this.customerForm.email().markAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');

    const model = this.customerModel();

    const request: CreateCustomerRequest = {
      firstName: model.firstName,
      lastName: model.lastName,
      email: model.email,
      phoneNumber: model.phoneNumber || null
    };

    this.customerService.createCustomer(request)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (customer) => {
          console.log('Customer created:', customer);
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          console.log('Error creating customer: ', error);
          this.errorMessage.set('Unable to create customer.');
        }
      });




  }




}
