import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Rol } from '../models/rol';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  private readonly http = inject(HttpClient)

  // obtenemos todos los alumnos
  getRoles(): Observable<Rol[]> {
    const url = 'http://localhost:5065/getAllRoles';
    return this.http.get<Rol[]>(url);
  }
  
}
