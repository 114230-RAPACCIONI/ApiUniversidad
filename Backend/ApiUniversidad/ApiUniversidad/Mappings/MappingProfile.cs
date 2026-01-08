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
        
        CreateMap<AlumnosPorCurso, AlumnosPorCursoDto>().ReverseMap();
        
        CreateMap<DocentesPorCurso, DocentesPorCursoDto>().ReverseMap();
        
        CreateMap<Curso, CursoDto>()
            .ForMember(dest => dest.NombreCarrera, opt => opt.MapFrom(src => src.IdCarreraNavigation.Nombre))
            .ReverseMap();
        
        CreateMap<NuevoCursoQuery, Curso>().ReverseMap();
        
        CreateMap<CarrerasUniversidad, CarreraDto>().ReverseMap();
        
        CreateMap<Role, RoleDto>().ReverseMap();
    }
    
}