import { Curso } from "./curso";
import { Docente } from "./docente";

export class DocentesPorCurso {

    id?: string;
    idCurso: string = '';
    idDocente: string = '';
    fechaAlta: Date = new Date();
    idDocenteNavigation: Docente = new Docente();
    idCursoNavigation: Curso = new Curso();

}
