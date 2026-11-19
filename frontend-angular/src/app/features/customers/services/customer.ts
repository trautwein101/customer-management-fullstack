import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

import { Customer } from "../models/customer.model";

@Injectable({
    providedIn: 'root'
})
export class CustomerService{
    private readonly http = inject(HttpClient);

    private readonly apiUrl = 'https://localhost:7066/api/Customers';

    getCustomers(): Observable<Customer[]> {
        return this.http.get<Customer[]>(this.apiUrl);
    }

}