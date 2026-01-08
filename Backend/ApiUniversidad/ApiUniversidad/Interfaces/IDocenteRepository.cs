using ApiUniversidad.Models;

namespace ApiUniversidad.Interfaces;

public interface IDocenteRepository
{
    Task<List<Docente>> GetAllDocentes();
    Task<Docente> GetDocenteById(Guid id);
    Task<Docente> CreateDocente(Docente docente);
    Task<Docente> UpdateDocente(Guid id, Docente docente);
    Task<bool> DeleteDocente(Guid id);
}
