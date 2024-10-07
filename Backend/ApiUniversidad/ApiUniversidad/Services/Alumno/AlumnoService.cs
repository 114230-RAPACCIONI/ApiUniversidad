using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Services.Alumno;

public class AlumnoService : IAlumnoService
{
    
    private readonly IAlumnoRepository _alumnoRepository;

    public AlumnoService(IAlumnoRepository alumnoRepository)
    {
        _alumnoRepository = alumnoRepository;
    }

    public async Task<ApiResponse<List<AlumnoDto>>> getAllAlumnos()
    {
        // buscamos todos los alumnos
        var alumnos = await _alumnoRepository.getAllAlumnos();
        
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
                FechaAlta = DateTime.UtcNow
            }).ToList();

        // retornamos la respuesta
        return new ApiResponse<List<AlumnoDto>>
        {
            Data = alumnoDto
        };
    }

    public async Task<ApiResponse<AlumnoDto>> getAlumnoById(Guid id)
    {
        // buscamos el alumno mediante el ID
        var alumno = await _alumnoRepository.getAlumnoById(id);
        
        if (alumno == null)
        {
            throw new Exception("El alumno no existe");
        }

        // mapeamos el alumno buscado a alumnoDto
        var alumnoDto = new AlumnoDto
        {
            Id = alumno.Id,
            Nombre = alumno.Nombre,
            Apellido = alumno.Apellido,
            Legajo = alumno.Legajo,
            Role = new RoleDto
            {
                Descripcion = alumno.IdRolNavigation.Descripcion
            },
            FechaAlta = DateTime.UtcNow
        };

        // retornamos la respuesta
        return new ApiResponse<AlumnoDto>
        {
            Data = alumnoDto
        };
    }
    
    public async Task<ApiResponse<AlumnoDto>> createAlumno(NuevoAlumnoQuery nuevoAlumno)
    {
        // Validar los datos del alumno
        if (string.IsNullOrEmpty(nuevoAlumno.Nombre) || string.IsNullOrEmpty(nuevoAlumno.Apellido) || string.IsNullOrEmpty(nuevoAlumno.Legajo))
        {
            return new ApiResponse<AlumnoDto>
            {
                Success = false,
                ErrorMessage = "Los datos del alumno son inválidos."
            };
        }

        // Crear el alumno en la base de datos
        var alumno = new Models.Alumno
        {
            Id = nuevoAlumno.Id,
            Nombre = nuevoAlumno.Nombre,
            Apellido = nuevoAlumno.Apellido,
            Legajo = nuevoAlumno.Legajo,
            IdRol = nuevoAlumno.IdRol,
            FechaAlta = DateTime.UtcNow // Asigna la fecha de alta
        };

        await _alumnoRepository.createAlumno(alumno);
        return new ApiResponse<AlumnoDto>
        {
            Success = true,
            Data = new AlumnoDto { Id = alumno.Id, Nombre = alumno.Nombre, Apellido = alumno.Apellido }
        };

    }

    public async Task<ApiResponse<AlumnoDto>> updateAlumno(Guid id, NuevoAlumnoQuery nuevoAlumnoQueryUpdate)
    {
        if (nuevoAlumnoQueryUpdate == null)
        {
            throw new Exception("El alumno es nulo");
        }

        // verificamos si el alumno existe en la base de datos
        var alumnoExistente = await _alumnoRepository.getAlumnoById(id);
        if (alumnoExistente == null)
        {
            throw new Exception("El alumno no existe");
        }
        
        // actualizamos los datos del alumno existente
        alumnoExistente.Nombre = nuevoAlumnoQueryUpdate.Nombre;
        alumnoExistente.Apellido = nuevoAlumnoQueryUpdate.Apellido;
        alumnoExistente.Legajo = nuevoAlumnoQueryUpdate.Legajo;
        alumnoExistente.IdRol = nuevoAlumnoQueryUpdate.IdRol;
        alumnoExistente.FechaAlta = nuevoAlumnoQueryUpdate.FechaAlta;

        // actualizamos los datos en la base de datos
        await _alumnoRepository.updateAlumno(alumnoExistente.Id, alumnoExistente);
        
        // mapeamos el alumno actualizado a alumnoDto
        AlumnoDto alumnoDto = new AlumnoDto
        {
            Nombre = alumnoExistente.Nombre,
            Apellido = alumnoExistente.Apellido,
            Legajo = alumnoExistente.Legajo,
            Role = new RoleDto
            {
                Descripcion = alumnoExistente.IdRolNavigation.Descripcion
            },
            FechaAlta = alumnoExistente.FechaAlta
        };

        // retornamos la respuesta
        return new ApiResponse<AlumnoDto>
        {
            Data = alumnoDto
        };
    }

    public async Task<ApiResponse<HttpStatusCode>> deleteAlumno(Guid id)
    {
        
        bool result = await _alumnoRepository.deleteAlumno(id);

        if (result)
        {
            return new ApiResponse<HttpStatusCode>
            {
                Data = HttpStatusCode.OK
            };
        }
        else 
        {
            throw new Exception("Alumno no encontrado");
        }
    }
}