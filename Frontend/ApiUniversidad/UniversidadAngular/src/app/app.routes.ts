import { Routes } from '@angular/router';
import { AlumnosListComponent } from './components/alumnos/alumnos-list/alumnos-list.component';
import { AlumnosFormComponent } from './components/alumnos/alumnos-form/alumnos-form.component';
import { AlumnosDetailComponent } from './components/alumnos/alumnos-detail/alumnos-detail.component';

export const routes: Routes = [

    {path: 'list', component: AlumnosListComponent},
    {path: 'alumnos', component: AlumnosFormComponent},
    {path: 'alumnos/:id', component: AlumnosFormComponent},
    {path: 'detail/:id' , component : AlumnosDetailComponent},
    {path: '', redirectTo: '/alumnos', pathMatch: 'full'},
    { path: '**', redirectTo: '/list' } 

];

