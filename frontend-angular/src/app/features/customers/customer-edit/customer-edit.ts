import { Component, inject, signal, OnInit } from '@angular/core';
import { form, FormField, required, email } from '@angular/forms/signals';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CustomerService } from '../services/customer'; 
import { finalize } from 'rxjs';
import { UpdateCustomerRequest } from '../models/customer.model';

@Component({
  imports: [FormField, RouterLink],
  selector: 'app-customer-edit',
  styleUrl: './customer-edit.scss',
  templateUrl: './customer-edit.html',
})
export class CustomerEdit {
  private readonly customerService = inject(CustomerService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  customerId = 0;

  customerModel = signal({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    isActive: true
  });

  customerForm = form(this.customerModel, (path) => {
    required(path.firstName);
    required(path.lastName);
    required(path.email);
    email(path.email);
  })

  isLoading = signal(false);
  errorMessage = signal('');

  ngOnInit(): void {
    this.customerId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadCustomer();
  }


  loadCustomer(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.customerService.getCustomerById(this.customerId)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (customer) => {
          this.customerModel.set({
            firstName: customer.firstName,
            lastName: customer.lastName,
            email: customer.email,
            phoneNumber: customer.phoneNumber ?? '',
            isActive: customer.isActive
          });
        },
        error: (error) => {
          console.error('Error laoding customer:', error);
          this.errorMessage.set('Unable to load customer.');
        }
      });
    }


    saveCustomer(): void {
        if (this.customerForm().invalid()) {
        this.customerForm.firstName().markAsTouched();
        this.customerForm.lastName().markAsTouched();
        this.customerForm.email().markAsTouched();
        return;
      }

      this.isLoading.set(true);
      this.errorMessage.set('');

      const model = this.customerModel();

      const request: UpdateCustomerRequest = {
        firstName: model.firstName,
        lastName: model.lastName,
        email: model.email,
        phoneNumber: model.phoneNumber || null,
        isActive: model.isActive
      };

      this.customerService.updateCustomer(this.customerId, request)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: () => {
          this.router.navigate(['/customers']);
        },
        error: (error) => {
          console.error('Error updating customer:', error);
          this.errorMessage.set('Unable to udpate customer.');
        }
      });
    }

}
