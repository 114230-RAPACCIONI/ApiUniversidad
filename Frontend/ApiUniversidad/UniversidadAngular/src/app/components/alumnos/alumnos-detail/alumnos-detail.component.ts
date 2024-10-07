import { Component, inject, Input, OnInit } from '@angular/core';
import { Alumno } from '../../../models/alumno';
import { AlumnosService } from '../../../services/alumnos.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alumnos-detail',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './alumnos-detail.component.html',
  styleUrl: './alumnos-detail.component.css'
})
export class AlumnosDetailComponent implements OnInit {

  alumno: Alumno = new Alumno();

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
    this.aluService.getAlumnoById(id).subscribe((data) => {
      this.alumno = data;
    });
  }
}
