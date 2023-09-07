using Entities;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IAreaRepository Areas {get;}
    IAreaUsuarioRepository AreaUsuarios {get;}
    ICategoriaContactoRepository CategoriaContactos {get;}
    ICiudadRepository Ciudades {get;}
    IContactoRepository Contactos {get;}
    IDetalleIncidenciaRepository DetalleIncidencias {get;}
    IDireccionRepository Direcciones {get;}
    IEstadoRepository Estados {get;}
    IIncidenciaRepository Incidencias {get;}
    ILugarRepository Lugares {get;}
    INivelIncidenciaRepository NivelIncidencias {get;}
    IPaisRepository Paises {get;}
    IPerisfericoRepository Perisfericos {get;}
    IPersonaRepository Personas {get;}
    IRegionRepository Regiones {get;}
    ITipoContactoRepository TipoContactos {get;}
    ITipoDocumentoRepository TipoDocumentos {get;}
    ITipoIncidenciaRepository TiposIncidencias {get;}
    IRolRepository Roles { get; }
    IUsuarioRepository Usuarios { get; }
    Task<int> SaveAsync();
}
