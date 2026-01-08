import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Alumno } from '../models/alumno';
import { AsignarAlumnoQuery, AsignarDocenteQuery } from '../models/asignacion';
import { Curso } from '../models/curso';
import { NuevoCurso } from '../models/nuevoCurso';

@Injectable({
  providedIn: 'root'
})
export class CursosService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5065';

  getAllCursos(): Observable<ApiResponse<Curso[]>> {
    const url = `${this.baseUrl}/cursos/getAllCursos`;
    return this.http.get<ApiResponse<Curso[]>>(url);
  }

  getCursoById(id: string): Observable<ApiResponse<Curso>> {
    const url = `${this.baseUrl}/cursos/getCursoById/${id}`;
    return this.http.get<ApiResponse<Curso>>(url);
  }

  createCurso(curso: NuevoCurso): Observable<ApiResponse<Curso>> {
    const url = `${this.baseUrl}/curso/crearCurso`;
    return this.http.post<ApiResponse<Curso>>(url, curso);
  }

  updateCurso(id: string, curso: NuevoCurso): Observable<ApiResponse<Curso>> {
    const url = `${this.baseUrl}/curso/updateCurso/${id}`;
    return this.http.put<ApiResponse<Curso>>(url, curso);
  }

  deleteCurso(id: string): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/curso/deleteCurso/${id}`;
    return this.http.delete<ApiResponse<any>>(url);
  }

  getAlumnosByCurso(idCurso: string): Observable<ApiResponse<Alumno[]>> {
    const url = `${this.baseUrl}/cursos/getAlumnosByCurso/${idCurso}`;
    return this.http.get<ApiResponse<Alumno[]>>(url);
  }

  asignarAlumnoACurso(asignacion: AsignarAlumnoQuery): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/curso/asignarAlumno`;
    return this.http.post<ApiResponse<any>>(url, asignacion);
  }

  quitarAlumnoDeCurso(idCurso: string, idAlumno: string): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/curso/quitarAlumno/${idCurso}/${idAlumno}`;
    return this.http.delete<ApiResponse<any>>(url);
  }

  asignarDocenteACurso(asignacion: AsignarDocenteQuery): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/curso/asignarDocente`;
    return this.http.post<ApiResponse<any>>(url, asignacion);
  }

  quitarDocenteDeCurso(idCurso: string, idDocente: string): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/curso/quitarDocente/${idCurso}/${idDocente}`;
    return this.http.delete<ApiResponse<any>>(url);
  }
}
