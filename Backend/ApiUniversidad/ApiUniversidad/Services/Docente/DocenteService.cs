using System.Net;
using ApiUniversidad.Dtos;
using ApiUniversidad.Interfaces;
using ApiUniversidad.Interfaces.Services;
using ApiUniversidad.Query;
using ApiUniversidad.Response;

namespace ApiUniversidad.Services.Docente;

public class DocenteService : IDocenteService
{
    private readonly IDocenteRepository _docenteRepository;

    public DocenteService(IDocenteRepository docenteRepository)
    {
        _docenteRepository = docenteRepository;
    }

    public async Task<ApiResponse<List<DocenteDto>>> GetAllDocentes()
    {
        var docentes = await _docenteRepository.GetAllDocentes();

        var docenteDto = docentes.Select(d =>
            new DocenteDto
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Apellido = d.Apellido,
                Legajo = d.Legajo,
                Role = new RoleDto
                {
                    Id = d.IdRolNavigation.Id,
                    Descripcion = d.IdRolNavigation.Descripcion
                },
                FechaAlta = d.FechaAlta
            }).ToList();

        return new ApiResponse<List<DocenteDto>>
        {
            Data = docenteDto
        };
    }

    public async Task<ApiResponse<DocenteDto>> GetDocenteById(Guid id)
    {
        var docente = await _docenteRepository.GetDocenteById(id);

        if (docente == null)
        {
            throw new Exception("El docente no existe");
        }

        var docenteDto = new DocenteDto
        {
            Id = docente.Id,
            Nombre = docente.Nombre,
            Apellido = docente.Apellido,
            Legajo = docente.Legajo,
            Role = new RoleDto
            {
                Id = docente.IdRolNavigation.Id,
                Descripcion = docente.IdRolNavigation.Descripcion
            },
            FechaAlta = docente.FechaAlta
        };

        return new ApiResponse<DocenteDto>
        {
            Data = docenteDto
        };
    }

    public async Task<ApiResponse<DocenteDto>> CreateDocente(NuevoDocenteQuery nuevoDocente)
    {
        if (string.IsNullOrEmpty(nuevoDocente.Nombre) || string.IsNullOrEmpty(nuevoDocente.Apellido) || string.IsNullOrEmpty(nuevoDocente.Legajo))
        {
            return new ApiResponse<DocenteDto>
            {
                Success = false,
                ErrorMessage = "Los datos del docente son inválidos."
            };
        }

        var docente = new Models.Docente
        {
            Id = nuevoDocente.Id,
            Nombre = nuevoDocente.Nombre,
            Apellido = nuevoDocente.Apellido,
            Legajo = nuevoDocente.Legajo,
            IdRol = nuevoDocente.IdRol,
            FechaAlta = DateTime.UtcNow
        };

        await _docenteRepository.CreateDocente(docente);

        var docenteCreado = await _docenteRepository.GetDocenteById(docente.Id);

        return new ApiResponse<DocenteDto>
        {
            Success = true,
            Data = new DocenteDto
            {
                Id = docenteCreado.Id,
                Nombre = docenteCreado.Nombre,
                Apellido = docenteCreado.Apellido,
                Legajo = docenteCreado.Legajo,
                Role = new RoleDto
                {
                    Id = docenteCreado.IdRolNavigation.Id,
                    Descripcion = docenteCreado.IdRolNavigation.Descripcion
                },
                FechaAlta = docenteCreado.FechaAlta
            }
        };
    }

    public async Task<ApiResponse<DocenteDto>> UpdateDocente(Guid id, NuevoDocenteQuery nuevoDocenteQueryUpdate)
    {
        if (nuevoDocenteQueryUpdate == null)
        {
            throw new Exception("El docente es nulo");
        }

        var docenteExistente = await _docenteRepository.GetDocenteById(id);
        if (docenteExistente == null)
        {
            throw new Exception("El docente no existe");
        }

        docenteExistente.Nombre = nuevoDocenteQueryUpdate.Nombre;
        docenteExistente.Apellido = nuevoDocenteQueryUpdate.Apellido;
        docenteExistente.Legajo = nuevoDocenteQueryUpdate.Legajo;
        docenteExistente.IdRol = nuevoDocenteQueryUpdate.IdRol;
        docenteExistente.FechaAlta = nuevoDocenteQueryUpdate.FechaAlta;

        await _docenteRepository.UpdateDocente(docenteExistente.Id, docenteExistente);

        var docenteActualizado = await _docenteRepository.GetDocenteById(id);

        DocenteDto docenteDto = new DocenteDto
        {
            Id = docenteActualizado.Id,
            Nombre = docenteActualizado.Nombre,
            Apellido = docenteActualizado.Apellido,
            Legajo = docenteActualizado.Legajo,
            Role = new RoleDto
            {
                Id = docenteActualizado.IdRolNavigation.Id,
                Descripcion = docenteActualizado.IdRolNavigation.Descripcion
            },
            FechaAlta = docenteActualizado.FechaAlta
        };

        return new ApiResponse<DocenteDto>
        {
            Data = docenteDto
        };
    }

    public async Task<ApiResponse<HttpStatusCode>> DeleteDocente(Guid id)
    {
        bool result = await _docenteRepository.DeleteDocente(id);

        if (result)
        {
            return new ApiResponse<HttpStatusCode>
            {
                Data = HttpStatusCode.OK
            };
        }
        else
        {
            throw new Exception("Docente no encontrado");
        }
    }
}
