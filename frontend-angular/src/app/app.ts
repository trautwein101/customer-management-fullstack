import { Component, inject } from "@angular/core";
import { RouterOutlet } from "@angular/router";

import { Router } from "@angular/router";
import { AuthService } from "./core/auth/services/auth.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})

export class App {

  private readonly authService  = inject(AuthService);
  private readonly router = inject(Router);

  title = "Customer Management";

  logout() : void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
