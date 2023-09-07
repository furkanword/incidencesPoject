using Dominio;
using AutoMapper;
using Entities;
using Api.Dtos;




namespace InsidenceAPI.Profiles;

public class MappingPofiles : Profile
    {
      public MappingPofiles(){
        CreateMap<Pais, PaisDto>().ReverseMap().ForMember(o => o.Regiones,d => d.Ignore());
        CreateMap<Region, RegionDto>().ReverseMap().ForMember(i => i.Ciudades,d => d.Ignore());
        CreateMap<Ciudad, CiudadDto>().ReverseMap();
        CreateMap<Persona, PersonDto>().ReverseMap();
        CreateMap<Incidencia, IncidenciaDto>().ReverseMap();
        CreateMap<Area, AreaDto>().ReverseMap();
        CreateMap<AreaUsuario, AreaUsuariosDto>().ReverseMap();
        CreateMap<CategoriaContacto ,CategoriaContactoDto>().ReverseMap();
        CreateMap<Contacto , ContactoDto>().ReverseMap();
        CreateMap<DetalleIncidencia, DetalleIncidenciaDto>().ReverseMap();
        CreateMap<Direccion ,DireccionDto>().ReverseMap();
        CreateMap<Estado , EstadoDto>().ReverseMap();
        CreateMap<Incidencia , IncidenciaDto> ().ReverseMap();
        CreateMap<Lugar , LugarDto>().ReverseMap();
        CreateMap<NivelIncidencia, NivelIncidenciaDto>().ReverseMap();
        CreateMap<Perisferico , PerisfericoDto>().ReverseMap();
        

        CreateMap<Area ,AreaxLugarDto>().ReverseMap();
        CreateMap<Incidencia, IncidenciaxEstado>().ReverseMap();
        CreateMap<Persona, PersonaxIncidencia>().ReverseMap();
        CreateMap<Region, RegionxCiudadDto>().ReverseMap();
        CreateMap<Pais, PaisxRegion>().ReverseMap();
      }
    }
