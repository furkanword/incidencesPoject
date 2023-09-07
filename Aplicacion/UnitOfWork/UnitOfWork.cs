using Dominio.Interfaces;
using Persistencia;
using Aplicacion.Repository;
using Entities;
using Dominio;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
        private readonly ApiContext _context;
        private DireccionRepository? _Direccion;
        private AreaRepository? _Area;
        private TipoContactoRepository? _TipoContacto;
        private AreaUsuarioRepository ?_AreaUsuario;
        private CategoriaContactoRepository? _CategoriaContacto;
        private CiudadRepository? _Ciudad;
        private ContactoRepository? _Contacto;
        private PaisRepository? _Pais;
        private DetalleIncidenciaRepository ?_DetalleIncidencia;
        private TipoDocumentoRepository ?_TipoDocumento;
        private IncidenciaRepository? _Incidencia;
        private NivelIncidenciaRepository ?_NivelIncidencia;
        private PerisfericoRepository? _Perisferico;
        private LugarRepository ?_Lugar;
        private RegionRepository ?_Region;
        private RolRepository? _Rol;
        private EstadoRepository ?_Estado;
        private TipoIncidenciaRepository ?_TipoIncidencia;
        private UsuarioRepository ?_Usuario;
        private PersonaRepository ? _Persona;
    


        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }


        public IPersonaRepository Personas
        {
            get
            {
                if (_Persona is not null)
                {
                    return _Persona;
                }
                return _Persona = new PersonaRepository(_context);
            }
        }


        public IDireccionRepository Direcciones
        {
            get
            {
                if (_Direccion is not null)
                {
                    return _Direccion;
                }
                return _Direccion = new DireccionRepository(_context);
            }
        }


        public ITipoContactoRepository TipoContactos
        {
            get
            {
                if (_TipoContacto is not null)
                {
                    return _TipoContacto;
                }
                return _TipoContacto = new TipoContactoRepository(_context);
            }
        }


        public IDetalleIncidenciaRepository DetalleIncidencias
        {
            get
            {
                if (_DetalleIncidencia is not null)
                {
                    return _DetalleIncidencia;
                }
                return _DetalleIncidencia = new DetalleIncidenciaRepository(_context);
            }
        }


        public IAreaRepository Areas
        {
            get
            {
                if (_Area is not null)
                {
                    return _Area;
                }
                return _Area = new AreaRepository(_context);
            }
        }

        public IAreaUsuarioRepository AreaUsuarios
        {
            get
            {
                if (_AreaUsuario is not null)
                {
                    return _AreaUsuario;
                }
                return _AreaUsuario = new AreaUsuarioRepository(_context);
            }
        }

        public ICategoriaContactoRepository CategoriaContactos
        {
            get
            {
                if (_CategoriaContacto is not null)
                {
                    return _CategoriaContacto;
                }
                return _CategoriaContacto = new CategoriaContactoRepository(_context);
            }
        }

        public ICiudadRepository Ciudades
        {
            get
            {
                if (_Ciudad is not null)
                {
                    return _Ciudad;
                }
                return _Ciudad = new CiudadRepository(_context);
            }
        }

        public IContactoRepository Contactos
        {
            get
            {
                if (_Contacto is not null)
                {
                    return _Contacto;
                }
                return _Contacto = new ContactoRepository(_context);
            }
        }

        public IPaisRepository Paises
        {
            get
            {
                if (_Pais is not null)
                {
                    return _Pais;
                }
                return _Pais = new PaisRepository(_context);
            }
        }
        public ITipoDocumentoRepository TipoDocumentos
        {
            get
            {
                if (_TipoDocumento is not null)
                {
                    return _TipoDocumento;
                }
                return _TipoDocumento = new TipoDocumentoRepository(_context);
            }
        }
        public IIncidenciaRepository Incidencias
        {
            get
            {
                if (_Incidencia is not null)
                {
                    return _Incidencia;
                }
                return _Incidencia = new IncidenciaRepository(_context);
            }
        }

        public INivelIncidenciaRepository NivelIncidencias
        {
            get
            {
                if (_NivelIncidencia is not null)
                {
                    return _NivelIncidencia;
                }
                return _NivelIncidencia = new NivelIncidenciaRepository(_context);
            }
        }

        public IPerisfericoRepository Perisfericos
        {
            get
            {
                if (_Perisferico is not null)
                {
                    return _Perisferico;
                }
                return _Perisferico = new PerisfericoRepository(_context);
            }
        }

        public ILugarRepository Lugares
        {
            get
            {
                if (_Lugar is not null)
                {
                    return _Lugar;
                }
                return _Lugar = new LugarRepository(_context);
            }
        }
        public IRegionRepository Regiones
        {
            get
            {
                if (_Region is not null)
                {
                    return _Region;
                }
                return _Region = new RegionRepository(_context);
            }
        }

        public IRolRepository Roles
        {
            get
            {
                if (_Rol is not null)
                {
                    return _Rol;
                }
                return _Rol = new RolRepository(_context);
            }
        }

        public IEstadoRepository Estados
        {
            get
            {
                if (_Estado is not null)
                {
                    return _Estado;
                }
                return _Estado = new EstadoRepository(_context);
            }
        }

        public ITipoIncidenciaRepository TiposIncidencias
        {
            get
            {
                if (_TipoIncidencia is not null)
                {
                    return _TipoIncidencia;
                }
                return _TipoIncidencia = new TipoIncidenciaRepository(_context);
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                if (_Usuario is not null)
                {
                    return _Usuario;
                }
                return _Usuario = new UsuarioRepository(_context);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync(){
            return await _context.SaveChangesAsync();
        }
}

     
