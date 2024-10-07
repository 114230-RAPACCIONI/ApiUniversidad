import { Rol } from "./rol";

export interface Usuario {

    id: string;
    email: string;
    contrasenia: string;
    idRol: string;
    fechaAlta: Date;
    idRolNavigation: Rol;
    
}
