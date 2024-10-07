import { DocentesPorCurso } from "./docentes-por-curso";
import { Rol } from "./rol";

export class Docente {

    id?: string;
    nombre: string = '';
    apellido: string = '';
    legajo: string = '';
    idRol: string = '';
    fechaAlta: Date = new Date();
    docentesPorCurso: DocentesPorCurso[] = [];
    idRolNavigation: Rol = new Rol();

}
