using ApiUniversidad.Interfaces;
using ApiUniversidad.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Repositories;

public class AlumnoRepository : IAlumnoRepository
{
    private readonly UniversidadContext _context;

    public AlumnoRepository(UniversidadContext context)
    {
        _context = context;
    }

    public async Task<List<Alumno>> getAllAlumnos()
    {
        return await _context.Alumnos
            .Include(a=>a.IdRolNavigation)
            .ToListAsync();
    }

    public async Task<Alumno> getAlumnoById(Guid id)
    {
        var alumno = await _context.Alumnos
            .Where(a => a.Id.Equals(id))
            .Include(a=>a.IdRolNavigation)
            .FirstOrDefaultAsync();
        if (alumno != null)
        {
            return alumno;
        }
        
        throw new Exception("Alumno no encontrado");
    }

    public async Task<Alumno> createAlumno(Alumno alumno)
    {
        if (alumno == null)
        {
            throw new Exception("Alumno es nulo");
        }

        await _context.AddAsync(alumno);
        await _context.SaveChangesAsync();

        return alumno;
    }

    public async Task<Alumno> updateAlumno(Guid id, Alumno alumno)
    {
        if (alumno == null)
        {
            throw new Exception("Alumno es nulo");
        }

        // se usa AsNoTracking para una entidad si esta siendo rastreada por el contexto 
        var alumnoAnterior = await _context.Alumnos.AsNoTracking()
            .Where(a=>a.Id.Equals(id))
            .Include(d => d.IdRolNavigation)
            .FirstOrDefaultAsync();
        if (alumnoAnterior == null)
        {
            throw new Exception("Alumno no encontrado");
        }

        alumnoAnterior.Nombre = alumno.Nombre;
        alumnoAnterior.Apellido = alumno.Apellido;
        alumnoAnterior.Legajo = alumno.Legajo;
        alumnoAnterior.IdRol = alumno.IdRol;
        alumnoAnterior.FechaAlta = DateTime.UtcNow; // Usar DateTime.UtcNow para actualizar la fecha de alta

        _context.Alumnos.Update(alumno);
        await _context.SaveChangesAsync();

        return alumno;
    }

    public async Task<bool> deleteAlumno(Guid id)
    {
        var alumno = await _context.Alumnos.FindAsync(id);
        if (alumno == null)
        {
            throw new Exception("Alumno no encontrado");
        }

        _context.Alumnos.Remove(alumno);
        await _context.SaveChangesAsync();

        return true;
    }
}