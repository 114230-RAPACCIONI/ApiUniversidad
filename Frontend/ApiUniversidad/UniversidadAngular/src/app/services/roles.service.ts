import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Rol } from '../models/rol';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5065';

  // obtenemos todos los roles
  getRoles(): Observable<ApiResponse<Rol[]>> {
    const url = `${this.baseUrl}/getAllRoles`;
    return this.http.get<ApiResponse<Rol[]>>(url);
  }
}
