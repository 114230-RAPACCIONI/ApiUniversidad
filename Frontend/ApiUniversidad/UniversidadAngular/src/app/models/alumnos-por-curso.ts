import { Alumno } from "./alumno";
import { Curso } from "./curso";

export class AlumnosPorCurso {

    id?: string;
    idCurso: string = '';
    idAlumno: string = '';
    fechaAlta: Date = new Date();
    idAlumnoNavigation: Alumno = new Alumno();
    idCursoNavigation: Curso = new Curso();
    
}
