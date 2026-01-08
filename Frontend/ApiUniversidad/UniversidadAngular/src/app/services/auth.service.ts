import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { LoginDto, LoginUsuarioQuery } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly baseUrl = 'http://localhost:5065';
  private readonly tokenKey = 'auth_token';

  login(loginQuery: LoginUsuarioQuery): Observable<ApiResponse<LoginDto>> {
    const url = `${this.baseUrl}/login`;
    return this.http.post<ApiResponse<LoginDto>>(url, loginQuery).pipe(
      tap(response => {
        if (response.success && response.data?.token) {
          this.setToken(response.data.token);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getTokenFromStorage(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
