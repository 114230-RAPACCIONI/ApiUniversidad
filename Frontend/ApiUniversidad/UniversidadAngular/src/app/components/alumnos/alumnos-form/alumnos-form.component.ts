import { Component, inject, OnInit } from '@angular/core';
import { Alumno } from '../../../models/alumno';
import { AlumnosService } from '../../../services/alumnos.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { NuevoAlumno } from '../../../models/nuevoAlumno';

@Component({
  selector: 'app-alumnos-form',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './alumnos-form.component.html',
  styleUrl: './alumnos-form.component.css'
})
export class AlumnosFormComponent implements OnInit {

  alumno: NuevoAlumno = {
    nombre: '',
    apellido: '',
    legajo: '',
    idRol: 'c56a4180-65aa-42ec-a945-5fd21dec0540',
    fechaAlta: new Date()
  };

  isEdit: boolean = false;

  private readonly alumnoService = inject(AlumnosService);
  private readonly router = inject(Router)
  private readonly activateRouter = inject(ActivatedRoute)

  ngOnInit(): void {
    const id = this.activateRouter.snapshot.paramMap.get('id');
    if (id) {
      this.getById(id);
    }
  }

  getById(id: string): void {
    this.isEdit = true;
    this.alumnoService.getAlumnoById(id).subscribe({
      next: (response) => {
        if (response.data) {
          this.alumno = {
            id: response.data.id,
            nombre: response.data.nombre,
            apellido: response.data.apellido,
            legajo: response.data.legajo,
            idRol: response.data.role?.id || response.data.idRol || '',
            fechaAlta: response.data.fechaAlta
          };
        }
      },
      error: (error) => {
        console.error('Error fetching alumno', error);
      }
    });
  }

  sendForm(form: NgForm) {
    if (form.valid) {
      if (this.isEdit && this.alumno.id) {
        this.alumnoService.updateAlumno(this.alumno.id, this.alumno).subscribe({
          next: (response) => {
            if (response.success) {
              alert("Alumno actualizado correctamente");
              this.router.navigate(['list']);
            }
          },
          error: (err) => {
            console.error('Error actualizando alumno', err);
            alert("Error al actualizar el alumno");
          }
        });
      } else {
        this.alumno.id = crypto.randomUUID();
        this.alumnoService.createAlumno(this.alumno).subscribe({
          next: (response) => {
            if (response.success) {
              alert("Alumno creado correctamente");
              this.router.navigate(['list']);
            }
          },
          error: (err) => {
            console.error('Error creando alumno', err);
            alert("Error al crear el alumno");
          }
        });
      }
      form.resetForm();
    }
  }

}
