using ApiUniversidad.Interfaces;
using ApiUniversidad.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly UniversidadContext _context;

    public CursoRepository(UniversidadContext context)
    {
        _context = context;
    }

    public async Task<List<Curso>> GetAllCursos()
    {
        return await _context.Cursos
            .Include(c => c.IdCarreraNavigation)
            .ToListAsync();
    }

    public async Task<Curso> GetCursoById(Guid id)
    {
        var curso = await _context.Cursos
            .Where(c => c.Id.Equals(id))
            .Include(c => c.IdCarreraNavigation)
            .FirstOrDefaultAsync();
        
        if (curso != null)
        {
            return curso;
        }
        
        throw new Exception("Curso no encontrado");
    }

    public async Task<Curso> CreateCurso(Curso curso)
    {
        if (curso == null)
        {
            throw new Exception("Curso es nulo");
        }

        await _context.AddAsync(curso);
        await _context.SaveChangesAsync();

        return curso;
    }

    public async Task<Curso> UpdateCurso(Guid id, Curso curso)
    {
        if (curso == null)
        {
            throw new Exception("Curso es nulo");
        }

        var cursoAnterior = await _context.Cursos.AsNoTracking()
            .Where(c => c.Id.Equals(id))
            .Include(c => c.IdCarreraNavigation)
            .FirstOrDefaultAsync();
        
        if (cursoAnterior == null)
        {
            throw new Exception("Curso no encontrado");
        }

        cursoAnterior.Nombre = curso.Nombre;
        cursoAnterior.Horarios = curso.Horarios;
        cursoAnterior.IdCarrera = curso.IdCarrera;

        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();

        return curso;
    }

    public async Task<bool> DeleteCurso(Guid id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            throw new Exception("Curso no encontrado");
        }

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Alumno>> GetAlumnosByCurso(Guid idCurso)
    {
        return await _context.AlumnosPorCursos
            .Where(ac => ac.IdCurso == idCurso)
            .Include(ac => ac.IdAlumnoNavigation)
                .ThenInclude(a => a.IdRolNavigation)
            .Select(ac => ac.IdAlumnoNavigation)
            .ToListAsync();
    }

    public async Task<AlumnosPorCurso> AsignarAlumnoACurso(Guid idCurso, Guid idAlumno)
    {
        // Verificar si ya está asignado
        var existeAsignacion = await _context.AlumnosPorCursos
            .AnyAsync(ac => ac.IdCurso == idCurso && ac.IdAlumno == idAlumno);

        if (existeAsignacion)
        {
            throw new Exception("El alumno ya está asignado a este curso");
        }

        var asignacion = new AlumnosPorCurso
        {
            Id = Guid.NewGuid(),
            IdCurso = idCurso,
            IdAlumno = idAlumno,
            FechaAlta = DateTime.UtcNow
        };

        await _context.AddAsync(asignacion);
        await _context.SaveChangesAsync();

        return asignacion;
    }

    public async Task<bool> QuitarAlumnoDeCurso(Guid idCurso, Guid idAlumno)
    {
        var asignacion = await _context.AlumnosPorCursos
            .FirstOrDefaultAsync(ac => ac.IdCurso == idCurso && ac.IdAlumno == idAlumno);

        if (asignacion == null)
        {
            throw new Exception("El alumno no está asignado a este curso");
        }

        _context.AlumnosPorCursos.Remove(asignacion);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<DocentesPorCurso> AsignarDocenteACurso(Guid idCurso, Guid idDocente)
    {
        // Verificar si ya está asignado
        var existeAsignacion = await _context.DocentesPorCursos
            .AnyAsync(dc => dc.IdCurso == idCurso && dc.IdDocente == idDocente);

        if (existeAsignacion)
        {
            throw new Exception("El docente ya está asignado a este curso");
        }

        var asignacion = new DocentesPorCurso
        {
            Id = Guid.NewGuid(),
            IdCurso = idCurso,
            IdDocente = idDocente,
            FechaAlta = DateTime.UtcNow
        };

        await _context.AddAsync(asignacion);
        await _context.SaveChangesAsync();

        return asignacion;
    }

    public async Task<bool> QuitarDocenteDeCurso(Guid idCurso, Guid idDocente)
    {
        var asignacion = await _context.DocentesPorCursos
            .FirstOrDefaultAsync(dc => dc.IdCurso == idCurso && dc.IdDocente == idDocente);

        if (asignacion == null)
        {
            throw new Exception("El docente no está asignado a este curso");
        }

        _context.DocentesPorCursos.Remove(asignacion);
        await _context.SaveChangesAsync();

        return true;
    }
}
