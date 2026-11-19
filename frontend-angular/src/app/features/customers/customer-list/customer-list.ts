import { Component, inject, OnInit, signal } from '@angular/core';
import { finalize } from 'rxjs';

import { CustomerService } from '../services/customer';
import { Customer } from '../models/customer.model';

@Component({
  selector: 'app-customer-list',
  imports: [],
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
        finalize(() => this.isLoading.set(false))
      )
      .subscribe({
        next: (data) => {
          this.customers.set(data);
        },
        error: (error) => {
          console.error('Error loading customers:', error);
          this.errorMessage.set('Unable to load customers');
        }
      });
  }
}
