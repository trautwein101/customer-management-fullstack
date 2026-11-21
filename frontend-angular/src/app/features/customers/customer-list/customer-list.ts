import { Component, inject, OnInit, signal } from '@angular/core';
import { catchError, finalize, of } from 'rxjs';
import { RouterLink } from '@angular/router';

import { CustomerService } from '../services/customer';
import { Customer } from '../models/customer.model';

@Component({
  selector: 'app-customer-list',
  imports: [RouterLink],
  templateUrl: './customer-list.html',
  styleUrl: './customer-list.scss',   
})
export class CustomerList implements OnInit {

  private readonly customerService = inject(CustomerService);

  customers = signal<Customer[]>([]);
  isLoading = signal(false);
  errorMessage = signal('');

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() : void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.customerService.getCustomers()
      .pipe(
          catchError((error) => {
            console.error('Error loading customers:', error);
            this.errorMessage.set('Unable to load customers.');

            return of([] as Customer[]);
        }),
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: (data) => {
          console.log('Customers received:', data);
          this.customers.set(data);
        },
        error: (error) => {
          console.error('Error loading customers:', error);
          this.errorMessage.set('Unable to load customers');
        }
      });
  }

  deleteCustomer(id: number): void {
    if(!confirm('Are you sure you want to delete this customer?')){
      return;
    }

    this.customerService.deleteCustomer(id)
    .subscribe({
      next: () => {
        this.loadCustomers();
      },
      error: (error) => {
        console.error('Error deleting customer:', error);
        this.errorMessage.set('Unable to delete customer.');
      }
    });

  }

}
