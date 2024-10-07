using ApiUniversidad.Dtos;
using ApiUniversidad.Models;
using ApiUniversidad.Query;
using AutoMapper;

namespace ApiUniversidad.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        
        CreateMap<Alumno, AlumnoDto>().ReverseMap();
        
        CreateMap<NuevoAlumnoQuery, Alumno>().ReverseMap();
        
        CreateMap<Docente, DocenteDto>().ReverseMap();
        
        CreateMap<NuevoDocenteQuery, Docente>().ReverseMap();
        
        CreateMap<AlumnosPorCurso, AlumnosPorCursoDto>()
            .ForMember(x=> x.IdAlumno,
                opt => opt.MapFrom(
                    src => src.IdAlumnoNavigation.AlumnosPorCursos.Count()))
            .ForMember(x=> x.IdAlumno,
                opt => opt.MapFrom(
                    src => src.IdAlumnoNavigation.Nombre))
            .ReverseMap();
        
        // CreateMap<DocentesPorCurso, Docen>()
        CreateMap<Role, RoleDto>().ReverseMap();
    }
    
}