import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { LoginUsuarioQuery } from '../../models/login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData: LoginUsuarioQuery = {
    nombreUsuario: '',
    email: ''
  };
  errorMessage: string = '';

  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.authService.login(this.loginData).subscribe({
        next: (response) => {
          if (response.success && response.data?.token) {
            this.router.navigate(['/list']);
          } else {
            this.errorMessage = response.errorMessage || 'Error al iniciar sesión';
          }
        },
        error: (err) => {
          console.error('Error en login', err);
          this.errorMessage = err.error?.errorMessage || 'Error al iniciar sesión';
        }
      });
    }
  }
}
