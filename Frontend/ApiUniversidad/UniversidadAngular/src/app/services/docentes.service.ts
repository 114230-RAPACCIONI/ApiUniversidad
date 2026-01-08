import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { Docente } from '../models/docente';
import { NuevoDocente } from '../models/nuevoDocente';

@Injectable({
  providedIn: 'root'
})
export class DocentesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5065';

  getAllDocentes(): Observable<ApiResponse<Docente[]>> {
    const url = `${this.baseUrl}/docentes/getAllDocentes`;
    return this.http.get<ApiResponse<Docente[]>>(url);
  }

  getDocenteById(id: string): Observable<ApiResponse<Docente>> {
    const url = `${this.baseUrl}/docentes/getDocenteById/${id}`;
    return this.http.get<ApiResponse<Docente>>(url);
  }

  createDocente(docente: NuevoDocente): Observable<ApiResponse<Docente>> {
    const url = `${this.baseUrl}/docente/crearDocente`;
    return this.http.post<ApiResponse<Docente>>(url, docente);
  }

  updateDocente(id: string, docente: NuevoDocente): Observable<ApiResponse<Docente>> {
    const url = `${this.baseUrl}/docente/updateDocente/${id}`;
    return this.http.put<ApiResponse<Docente>>(url, docente);
  }

  deleteDocente(id: string): Observable<ApiResponse<any>> {
    const url = `${this.baseUrl}/docente/deleteDocente/${id}`;
    return this.http.delete<ApiResponse<any>>(url);
  }
}
