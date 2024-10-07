using ApiUniversidad.Models;

namespace ApiUniversidad.Interfaces;

public interface IAlumnoRepository
{
    Task<List<Alumno>> getAllAlumnos();
    Task<Alumno> getAlumnoById(Guid id);
    Task<Alumno> createAlumno(Alumno alumno);
    Task<Alumno> updateAlumno(Guid id, Alumno alumno);
    Task<bool> deleteAlumno(Guid id);
}