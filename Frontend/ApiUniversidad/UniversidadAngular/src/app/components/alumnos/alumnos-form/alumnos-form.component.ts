import { Component, inject, NgModule, OnInit } from '@angular/core';
import { Alumno } from '../../../models/alumno';
import { AlumnosService } from '../../../services/alumnos.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Subscriber, Subscription } from 'rxjs';
import { NuevoAlumno } from '../../../models/nuevoAlumno';

@Component({
  selector: 'app-alumnos-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
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
  private subscription = new Subscription();

  private readonly alumnoService = inject(AlumnosService);
  private readonly router = inject(Router)
  private readonly activateRouter = inject(ActivatedRoute)

  ngOnInit(): void {
    const id = this.activateRouter.snapshot.paramMap.get('id');
    if (id) {
      this.getById(id);
    } else {
      console.error('ID is undefined');
    }
  }

  getById(id: string): void {
  this.alumnoService.getAlumnoById(id).subscribe(
    (response) => {
      this.alumno = response;
    },
    (error) => {
      console.error('Error fetching alumno', error);
    }
  );
}

//   sendForm(form: NgForm) {
//     if (form.valid) {
//       if (this.isEdit) {
//         this.subscription.add(
//         this.alumnoService.updateAlumno(this.alumno).subscribe({
//           next: (data) => alert("Alumno actualizado correctamente" + data.id),
//           error: (errr) => alert("Error al actualizar el alumno."),
//           complete: () => this.router.navigate(['list'])
//         }))
//       } else {
//         this.subscription.add(
//         this.alumnoService.createAlumno(this.alumno).subscribe({
//           next: (data) => alert("Alumno creado correctamente" + data.id),
//           error: (errr) => alert("Error al crear el alumno."),
//          complete: () => this.router.navigate(['list'])
//         }))
//       }

//       form.resetForm();
//       this.alumno = new Alumno();
//       console.log(this.alumno);
//     }
//   }


}
