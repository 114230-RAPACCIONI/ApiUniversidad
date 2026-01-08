import { AlumnosPorCurso } from "./alumnos-por-curso";
import { CarrerasUniversidad } from "./carreras-universidad";
import { DocentesPorCurso } from "./docentes-por-curso";

export class Curso {
    id?: string;
    nombre: string = '';
    fechaCreacion: Date = new Date();
    horarios: string = '';
    idCarrera: string = '';
    nombreCarrera?: string; // Para DTO del backend
    alumnosPorCurso?: AlumnosPorCurso[];
    docentesPorCurso?: DocentesPorCurso[];
    idCarreraNavigation?: CarrerasUniversidad;
}
