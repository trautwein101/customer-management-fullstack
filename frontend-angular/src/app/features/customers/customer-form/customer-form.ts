import { Component, input, output, signal, effect, computed } from '@angular/core';
import { form, FormField, required, email } from '@angular/forms/signals';
import { Customer, UpdateCustomerRequest } from '../models/customer.model';

@Component({
  selector: 'app-customer-form',
  imports: [FormField],
  templateUrl: './customer-form.html',
  styleUrl: './customer-form.scss'
})
export class CustomerForm {

  customer = input<Customer | null>(null);
  isLoading = input<boolean>(false);

  save = output<UpdateCustomerRequest>();

  constructor() {
    effect(() => {
      const customer = this.customer();

      if (customer) {
        this.customerModel.set({
          firstName: customer.firstName,
          lastName: customer.lastName,
          email: customer.email,
          phoneNumber: customer.phoneNumber ?? '',
          isActive: customer.isActive
        });
      }
    });
  }

  buttonText = computed(() =>
    this.customer() ? 'Save Changes' : 'Create Customer'
  );

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
  });


  submitForm(): void {
    if (this.customerForm().invalid()) {
      this.customerForm.firstName().markAsTouched();
      this.customerForm.lastName().markAsTouched();
      this.customerForm.email().markAsTouched();
      return;
    }

    const model = this.customerModel();

    const request: UpdateCustomerRequest = {
      firstName: model.firstName,
      lastName: model.lastName,
      email: model.email,
      phoneNumber: model.phoneNumber || null,
      isActive: model.isActive
    };

    this.save.emit(request);
  }


}