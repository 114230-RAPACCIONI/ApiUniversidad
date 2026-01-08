using ApiUniversidad.Models;

namespace ApiUniversidad.Interfaces;

public interface ICursoRepository
{
    Task<List<Curso>> GetAllCursos();
    Task<Curso> GetCursoById(Guid id);
    Task<Curso> CreateCurso(Curso curso);
    Task<Curso> UpdateCurso(Guid id, Curso curso);
    Task<bool> DeleteCurso(Guid id);
    Task<List<Alumno>> GetAlumnosByCurso(Guid idCurso);
    Task<AlumnosPorCurso> AsignarAlumnoACurso(Guid idCurso, Guid idAlumno);
    Task<bool> QuitarAlumnoDeCurso(Guid idCurso, Guid idAlumno);
    Task<DocentesPorCurso> AsignarDocenteACurso(Guid idCurso, Guid idDocente);
    Task<bool> QuitarDocenteDeCurso(Guid idCurso, Guid idDocente);
}
