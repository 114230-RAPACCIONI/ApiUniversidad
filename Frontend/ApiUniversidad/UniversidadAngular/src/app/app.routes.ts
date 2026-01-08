import { Routes } from '@angular/router';
import { AlumnosListComponent } from './components/alumnos/alumnos-list/alumnos-list.component';
import { AlumnosFormComponent } from './components/alumnos/alumnos-form/alumnos-form.component';
import { AlumnosDetailComponent } from './components/alumnos/alumnos-detail/alumnos-detail.component';
import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'list', component: AlumnosListComponent },
    { path: 'alumnos', component: AlumnosFormComponent },
    { path: 'alumnos/:id', component: AlumnosFormComponent },
    { path: 'detail/:id', component: AlumnosDetailComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];

