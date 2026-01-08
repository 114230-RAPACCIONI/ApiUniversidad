import { Component, inject, OnInit } from '@angular/core';
import { Alumno } from '../../../models/alumno';
import { AlumnosService } from '../../../services/alumnos.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alumnos-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './alumnos-detail.component.html',
  styleUrl: './alumnos-detail.component.css'
})
export class AlumnosDetailComponent implements OnInit {
  alumno: Alumno = {} as Alumno;

  private readonly aluService = inject(AlumnosService)
  private readonly activateRouter = inject(ActivatedRoute)


  ngOnInit(): void {
    this.activateRouter.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.getById(id);
      }
    });
  }

  getById(id: string): void {
    this.aluService.getAlumnoById(id).subscribe({
      next: (response) => {
        this.alumno = response.data || new Alumno();
      },
      error: (err) => {
        console.error('Error fetching alumno', err);
      }
    });
  }
}
