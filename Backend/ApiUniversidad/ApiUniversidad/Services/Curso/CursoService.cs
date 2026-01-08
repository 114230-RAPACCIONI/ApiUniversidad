using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Services.Curso;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;

    public CursoService(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<ApiResponse<List<CursoDto>>> GetAllCursos()
    {
        var cursos = await _cursoRepository.GetAllCursos();

        var cursoDto = cursos.Select(c =>
            new CursoDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.FechaCreacion,
                Horarios = c.Horarios,
                IdCarrera = c.IdCarrera,
                NombreCarrera = c.IdCarreraNavigation.Nombre
            }).ToList();

        return new ApiResponse<List<CursoDto>>
        {
            Data = cursoDto
        };
    }

    public async Task<ApiResponse<CursoDto>> GetCursoById(Guid id)
    {
        var curso = await _cursoRepository.GetCursoById(id);

        if (curso == null)
        {
            throw new Exception("El curso no existe");
        }

        var cursoDto = new CursoDto
        {
            Id = curso.Id,
            Nombre = curso.Nombre,
            FechaCreacion = curso.FechaCreacion,
            Horarios = curso.Horarios,
            IdCarrera = curso.IdCarrera,
            NombreCarrera = curso.IdCarreraNavigation.Nombre
        };

        return new ApiResponse<CursoDto>
        {
            Data = cursoDto
        };
    }

    public async Task<ApiResponse<CursoDto>> CreateCurso(NuevoCursoQuery nuevoCurso)
    {
        if (string.IsNullOrEmpty(nuevoCurso.Nombre) || string.IsNullOrEmpty(nuevoCurso.Horarios))
        {
            return new ApiResponse<CursoDto>
            {
                Success = false,
                ErrorMessage = "Los datos del curso son inválidos."
            };
        }

        var curso = new Models.Curso
        {
            Id = nuevoCurso.Id,
            Nombre = nuevoCurso.Nombre,
            FechaCreacion = nuevoCurso.FechaCreacion,
            Horarios = nuevoCurso.Horarios,
            IdCarrera = nuevoCurso.IdCarrera
        };

        await _cursoRepository.CreateCurso(curso);

        var cursoCreado = await _cursoRepository.GetCursoById(curso.Id);

        return new ApiResponse<CursoDto>
        {
            Success = true,
            Data = new CursoDto
            {
                Id = cursoCreado.Id,
                Nombre = cursoCreado.Nombre,
                FechaCreacion = cursoCreado.FechaCreacion,
                Horarios = cursoCreado.Horarios,
                IdCarrera = cursoCreado.IdCarrera,
                NombreCarrera = cursoCreado.IdCarreraNavigation.Nombre
            }
        };
    }

    public async Task<ApiResponse<CursoDto>> UpdateCurso(Guid id, NuevoCursoQuery nuevoCursoQueryUpdate)
    {
        if (nuevoCursoQueryUpdate == null)
        {
            throw new Exception("El curso es nulo");
        }

        var cursoExistente = await _cursoRepository.GetCursoById(id);
        if (cursoExistente == null)
        {
            throw new Exception("El curso no existe");
        }

        cursoExistente.Nombre = nuevoCursoQueryUpdate.Nombre;
        cursoExistente.Horarios = nuevoCursoQueryUpdate.Horarios;
        cursoExistente.IdCarrera = nuevoCursoQueryUpdate.IdCarrera;

        await _cursoRepository.UpdateCurso(cursoExistente.Id, cursoExistente);

        var cursoActualizado = await _cursoRepository.GetCursoById(id);

        var cursoDto = new CursoDto
        {
            Id = cursoActualizado.Id,
            Nombre = cursoActualizado.Nombre,
            FechaCreacion = cursoActualizado.FechaCreacion,
            Horarios = cursoActualizado.Horarios,
            IdCarrera = cursoActualizado.IdCarrera,
            NombreCarrera = cursoActualizado.IdCarreraNavigation.Nombre
        };

        return new ApiResponse<CursoDto>
        {
            Data = cursoDto
        };
    }

    public async Task<ApiResponse<HttpStatusCode>> DeleteCurso(Guid id)
    {
        bool result = await _cursoRepository.DeleteCurso(id);

        if (result)
        {
            return new ApiResponse<HttpStatusCode>
            {
                Data = HttpStatusCode.OK
            };
        }
        else
        {
            throw new Exception("Curso no encontrado");
        }
    }

    public async Task<ApiResponse<List<AlumnoDto>>> GetAlumnosByCurso(Guid idCurso)
    {
        var alumnos = await _cursoRepository.GetAlumnosByCurso(idCurso);

        var alumnoDto = alumnos.Select(a =>
            new AlumnoDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                Legajo = a.Legajo,
                Role = new RoleDto
                {
                    Id = a.IdRolNavigation.Id,
                    Descripcion = a.IdRolNavigation.Descripcion
                },
                FechaAlta = a.FechaAlta
            }).ToList();

        return new ApiResponse<List<AlumnoDto>>
        {
            Data = alumnoDto
        };
    }

    public async Task<ApiResponse<AlumnosPorCursoDto>> AsignarAlumnoACurso(AsignarAlumnoQuery asignarAlumnoQuery)
    {
        var asignacion = await _cursoRepository.AsignarAlumnoACurso(asignarAlumnoQuery.IdCurso, asignarAlumnoQuery.IdAlumno);

        var asignacionDto = new AlumnosPorCursoDto
        {
            Id = asignacion.Id,
            IdCurso = asignacion.IdCurso,
            IdAlumno = asignacion.IdAlumno,
            FechaAlta = asignacion.FechaAlta
        };

        return new ApiResponse<AlumnosPorCursoDto>
        {
            Success = true,
            Data = asignacionDto
        };
    }

    public async Task<ApiResponse<HttpStatusCode>> QuitarAlumnoDeCurso(Guid idCurso, Guid idAlumno)
    {
        bool result = await _cursoRepository.QuitarAlumnoDeCurso(idCurso, idAlumno);

        if (result)
        {
            return new ApiResponse<HttpStatusCode>
            {
                Data = HttpStatusCode.OK
            };
        }
        else
        {
            throw new Exception("No se pudo quitar el alumno del curso");
        }
    }

    public async Task<ApiResponse<DocentesPorCursoDto>> AsignarDocenteACurso(AsignarDocenteQuery asignarDocenteQuery)
    {
        var asignacion = await _cursoRepository.AsignarDocenteACurso(asignarDocenteQuery.IdCurso, asignarDocenteQuery.IdDocente);

        var asignacionDto = new DocentesPorCursoDto
        {
            Id = asignacion.Id,
            IdCurso = asignacion.IdCurso,
            IdDocente = asignacion.IdDocente,
            FechaAlta = asignacion.FechaAlta
        };

        return new ApiResponse<DocentesPorCursoDto>
        {
            Success = true,
            Data = asignacionDto
        };
    }

    public async Task<ApiResponse<HttpStatusCode>> QuitarDocenteDeCurso(Guid idCurso, Guid idDocente)
    {
        bool result = await _cursoRepository.QuitarDocenteDeCurso(idCurso, idDocente);

        if (result)
        {
            return new ApiResponse<HttpStatusCode>
            {
                Data = HttpStatusCode.OK
            };
        }
        else
        {
            throw new Exception("No se pudo quitar el docente del curso");
        }
    }
}
