import { Component, inject, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { AuthService } from '../../../core/auth/services/auth.service';
import { finalize } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormField],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  loginModel = signal({
    username: '',
    password: ''
  });

  loginForm = form(this.loginModel);

  isLoading = signal(false);
  errorMessage = signal('');


login(): void{
  this.isLoading.set(true);
  this.errorMessage.set('');

  const credentials =  this.loginModel();

  this.authService.login(credentials)
    .pipe(
      finalize(() => this.isLoading.set(false))
    )
    .subscribe({
      next: (response) => {
        this.authService.saveToken(response.accessToken);
        this.router.navigate(['/customers']);

      },
      error: (error) => {
        console.error('Login failed:', error);
        this.errorMessage.set('Invalid username or password');
      }
    });
}


}