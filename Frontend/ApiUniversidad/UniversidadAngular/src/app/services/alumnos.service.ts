import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Alumno } from '../models/alumno';
import { NuevoAlumno } from '../models/nuevoAlumno';

@Injectable({
  providedIn: 'root'
})
export class AlumnosService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5065';

  // obtenemos todos los alumnos
  getAllAlumnos(): Observable<ApiResponse<Alumno[]>> {
    const url = `${this.baseUrl}/alumnos/getAllAlumnos`;
    return this.http.get<ApiResponse<Alumno[]>>(url);
  }

  // obtenemos un alumno por su id
  getAlumnoById(id: string): Observable<ApiResponse<Alumno>> {
    const url = `${this.baseUrl}/alumnos/getAlumnosById/${id}`;
    return this.http.get<ApiResponse<Alumno>>(url);
  }

  // creamos un alumno
  createAlumno(alumno: NuevoAlumno): Observable<ApiResponse<Alumno>> {
    const url = `${this.baseUrl}/alumno/crearAlumno`;
    return this.http.post<ApiResponse<Alumno>>(url, alumno);
  }

  // actualizamos un alumno
  updateAlumno(id: string, alumno: NuevoAlumno): Observable<ApiResponse<Alumno>> {
    const url = `${this.baseUrl}/alumno/updateAlumno/${id}`;
    return this.http.put<ApiResponse<Alumno>>(url, alumno);
  }

  // eliminamos un alumno
  deleteAlumno(id: string): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/alumno/deleteAlumno/${id}`;
    return this.http.delete<ApiResponse<any>>(url);
  }
}
