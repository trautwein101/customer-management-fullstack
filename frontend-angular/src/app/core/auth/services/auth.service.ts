import {inject, Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/auth.model';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private readonly http = inject(HttpClient);

    private readonly apiUrl = 'https://localhost:7066/api/Auth';

    login(credentials: LoginRequest): Observable<LoginResponse>{
        return this.http.post<LoginResponse>(
            `${this.apiUrl}/login`,
            credentials
        )
    }

    saveToken(token: string): void {
        sessionStorage.setItem('authToken', token);
    }

    getToken(): string | null {
        return sessionStorage.getItem('authToken');
    }

    logout(): void {
        sessionStorage.removeItem('authToken');
    }


}