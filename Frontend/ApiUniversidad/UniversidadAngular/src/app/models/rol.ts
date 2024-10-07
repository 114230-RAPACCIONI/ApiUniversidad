import { Alumno } from "./alumno";
import { Docente } from "./docente";
import { Usuario } from "./usuario";

export class Rol {

    id?: string;
    nombre: string = '';
    descripcion: string = '';
    alumnos: Alumno[] = [];
    docentes: Docente[] = [];
    usuarios: Usuario[] = [];
    
}
