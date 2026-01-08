import { Component, inject, NgModule, OnInit } from '@angular/core';
import { Alumno } from '../../../models/alumno';
import { AlumnosService } from '../../../services/alumnos.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-alumnos-list',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './alumnos-list.component.html',
  styleUrl: './alumnos-list.component.css'
})
export class AlumnosListComponent implements OnInit {

  alumnos: Alumno[] = [];

  private readonly alumnoService = inject(AlumnosService)
  private readonly router = inject(Router);

  ngOnInit(): void {
    this.getAlumnos();
  }

  // obtenemos los alumnos del servicio
  getAlumnos() {
    this.alumnoService.getAllAlumnos().subscribe({
      next: (response) => {
        this.alumnos = response.data || [];
      },
      error: (err) => {
        console.error('Error fetching alumnos', err);
        this.alumnos = [];
      }
    });
  }


  // eliminamos el alumno
  eliminarAlumno(id?: string) {
    if (id == null) return;
    this.alumnoService.deleteAlumno(id).subscribe({
      next: (response) => {
        alert("Alumno eliminado correctamente");
        this.getAlumnos();
      },
      error: (err) => {
        console.error('Error eliminando alumno', err);
        alert("Error al eliminar el alumno");
      }
    });
  }

  // editamos el alumno
  editarAlumno(alumno: Alumno) {
    this.router.navigate(['alumnos', alumno.id])
  }

  // detalles de los alumnos
  detalles(id?: string) {
    if(id === null) return;
    this.router.navigate(['detail', id])
  }
}
