import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

import { Customer, CreateCustomerRequest, UpdateCustomerRequest } from "../models/customer.model";

@Injectable({
    providedIn: 'root'
})
export class CustomerService{
    private readonly http = inject(HttpClient);

    private readonly apiUrl = 'https://localhost:7066/api/Customers';

    getCustomers(): Observable<Customer[]> {
        return this.http.get<Customer[]>(this.apiUrl);
    }

    getCustomerById(id: number): Observable<Customer> {
        return this.http.get<Customer>(`${this.apiUrl}/${id}`)
    }

    createCustomer(customer: CreateCustomerRequest): Observable<Customer> {
        return this.http.post<Customer>(this.apiUrl, customer);
    }

    updateCustomer(
        id: number,
        customer: UpdateCustomerRequest) : Observable<void> {
            return this.http.put<void>(`${this.apiUrl}/${id}`, customer);     
    }

    deleteCustomer(id: number) : Observable<void>{
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }


}