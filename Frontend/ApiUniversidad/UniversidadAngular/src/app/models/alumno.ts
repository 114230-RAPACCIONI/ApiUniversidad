import { AlumnosPorCurso } from "./alumnos-por-curso";
import { Rol } from "./rol";

export class Alumno {

    id?: string;
    nombre: string = '';
    apellido: string = '';
    legajo: string = '';
    idRol: string = '';
    fechaAlta: Date = new Date();
    alumnosPorCurso: AlumnosPorCurso[] = [];
    idRolNavigation: Rol = new Rol();
    
}