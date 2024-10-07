import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Alumno } from '../models/alumno';

@Injectable({
  providedIn: 'root'
})
export class AlumnosService {

  private readonly http = inject(HttpClient)

  // obtenemos todos los alumnos
  getAllAlumnos(): Observable<Alumno[]> {
    const url = 'http://localhost:5065/alumnos/getAllAlumnos';
    return this.http.get<Alumno[]>(url);
  } 

  // obtenemos un alumno por su id
  getAlumnoById(id: string): Observable<Alumno> {
    const url = 'http://localhost:5065/alumnos/getAlumnosById/' + id;
    return this.http.get<Alumno>(url);
  }

  // creamos un alumno
  createAlumno(alumno: Alumno): Observable<Alumno> {
    const url = 'http://localhost:5065/alumno/crearAlumno';
    return this.http.post<Alumno>(url, alumno);
  }

  // actualizamos un alumno
  updateAlumno(alumno: Alumno): Observable<Alumno> {
    const url = 'http://localhost:5065/alumno/updateAlumno/' + alumno.id;
    return this.http.put<Alumno>(url, alumno)
  }

  // eliminamos un alumno
  deleteAlumno(id: string): Observable<Alumno> {
    const url = 'http://localhost:5065/alumno/deleteAlumno/' + id;
    return this.http.delete<Alumno>(url);
  }

}
