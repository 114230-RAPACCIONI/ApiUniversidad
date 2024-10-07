import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AlumnosListComponent } from "./components/alumnos/alumnos-list/alumnos-list.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,RouterModule, AlumnosListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Sistema de Universidad';
}
