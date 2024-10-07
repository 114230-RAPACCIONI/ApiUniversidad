import { Component, inject, OnInit } from '@angular/core';
import { Rol } from '../../../models/rol';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'app-roles-list',
  standalone: true,
  imports: [],
  templateUrl: './roles-list.component.html',
  styleUrl: './roles-list.component.css'
})
export class RolesListComponent implements OnInit{

  roles: Rol[] = [];

  private readonly rolService = inject(RolesService)

  ngOnInit(): void {
    this.getRoles();
  }

  getRoles(): void {
    this.rolService.getRoles().subscribe((data: Rol[]) => {
      this.rolService.getRoles().subscribe({
        next: (response: any) => {
          console.log('Datos recibidos:', response); 
          this.roles = response.data || [];
        },
        error: (err) => {
          console.error('Error fetching alumnos', err);
          this.roles = []; 
        }
      });
    });
  }

}
