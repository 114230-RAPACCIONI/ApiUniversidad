using ApiUniversidad.Interfaces;
using ApiUniversidad.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversidad.Repositories;

public class DocenteRepository : IDocenteRepository
{
    private readonly UniversidadContext _context;

    public DocenteRepository(UniversidadContext context)
    {
        _context = context;
    }

    public async Task<List<Docente>> GetAllDocentes()
    {
        return await _context.Docentes
            .Include(d => d.IdRolNavigation)
            .ToListAsync();
    }

    public async Task<Docente> GetDocenteById(Guid id)
    {
        var docente = await _context.Docentes
            .Where(d => d.Id.Equals(id))
            .Include(d => d.IdRolNavigation)
            .FirstOrDefaultAsync();
        
        if (docente != null)
        {
            return docente;
        }
        
        throw new Exception("Docente no encontrado");
    }

    public async Task<Docente> CreateDocente(Docente docente)
    {
        if (docente == null)
        {
            throw new Exception("Docente es nulo");
        }

        await _context.AddAsync(docente);
        await _context.SaveChangesAsync();

        return docente;
    }

    public async Task<Docente> UpdateDocente(Guid id, Docente docente)
    {
        if (docente == null)
        {
            throw new Exception("Docente es nulo");
        }

        var docenteAnterior = await _context.Docentes.AsNoTracking()
            .Where(d => d.Id.Equals(id))
            .Include(d => d.IdRolNavigation)
            .FirstOrDefaultAsync();
        
        if (docenteAnterior == null)
        {
            throw new Exception("Docente no encontrado");
        }

        docenteAnterior.Nombre = docente.Nombre;
        docenteAnterior.Apellido = docente.Apellido;
        docenteAnterior.Legajo = docente.Legajo;
        docenteAnterior.IdRol = docente.IdRol;
        docenteAnterior.FechaAlta = DateTime.UtcNow;

        _context.Docentes.Update(docente);
        await _context.SaveChangesAsync();

        return docente;
    }

    public async Task<bool> DeleteDocente(Guid id)
    {
        var docente = await _context.Docentes.FindAsync(id);
        if (docente == null)
        {
            throw new Exception("Docente no encontrado");
        }

        _context.Docentes.Remove(docente);
        await _context.SaveChangesAsync();

        return true;
    }
}
