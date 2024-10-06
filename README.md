# Escolar
## Aspectos a tener en cuenta para el backend
#### 1. Autenticar usuarios con JWT, tener en cuenta que se deberá agregar al jwt como CLAIM todos sus datos incluido el nombre del rol.
#### 2. Utilizar automapper
#### 3. Crear DTOs
#### 4. Crear repositorios (con sus respectivas interfaces)
#### 5. Crear servicios (con sus respectivas interfaces)
#### 6. Crear endpoints para ABM Cursos (rol admin)
#### 7. Crear endpoints para ABM Alumnos (para alta y baja rol admin, update rol alumno)
#### 8. Crear endpoints para ABM Docentes (para alta y baja rol admin, update rol docente)
#### 9. Crear endpoint para Login usuario (sin auth)
#### 10. Crear endpoint para obtener listado de alumnos por curso y alumno por id. (rol admin)
#### 11. Crear endpoint para asignar docente a un curso (rol admin)
#### 12. Crear endpoint para asignar alumno a curso (rol admin)
#### 13. Crear endpoint para quitar alumno a un curso (rol admin)
#### 14. Crear endpoint para quitar docente a un curso (rol admin)


## Modelos
***Usuarios** -> admin, profesor, alumno*  

`Id – Guid`  

`Email – string`  

`Contraseña – string`  

`IdRol – Guid - FK`  

**Roles**  

`Id – Guid`  

`Nombre – string `  

`Descripcion – string`  

**Cursos**  

`Id – Guid`  

`Nombre – string `  

`FechaCreacion – datetime `  

`Horarios – string`   

`IdCarrera – Guid – FK`  

**Carreras**  

`Id – Guid `  

`Nombre -string`  

**Docentes**  

`Id – Guid `  

`Nombre – string `  

`Apellido – string `  

`Legajo – string `  

`IdRol – Guid – FK`  

**Alumnos**  

`Id – Guid `  

`Nombre – string `  

`Apellido – string `  

`Legajo – string `  

`IdRol – Guid – FK`  

**DocentesXcursos**  

`Id – Guid`  

`IdCurso – Guid – Fk `  

`IdDocente – Guid – Fk `  

`FechaAlta – datetime`  

**AlumnosXCursos**  

`Id – Guid`  

`IdCurso – Guid – Fk `  

`IdAlumno – Guid – Fk `  

`FechaAlta – datetime`