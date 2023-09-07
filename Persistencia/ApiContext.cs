using System.Data.Common;
using System.Reflection;
using Dominio;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistencia
{
    public class ApiContext : DbContext
    {
        public  ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<Usuario> ? Usuarios { get; set; }
        public DbSet<Rol> ? Roles { get; set;}
        public DbSet<UsuarioRoles> ? UsuariosRoles { get; set; }   
        public DbSet<Persona> ? Personas {get; set;}
        public DbSet<TipoIncidencia> ? TipoIncidencias {get; set;}
        public DbSet<Estado> ? Estados {get; set;}
        public DbSet<Lugar> ? Lugares {get; set;}
        public DbSet<Perisferico> ? Perisfericos {get; set;}
        public DbSet<NivelIncidencia> ? NivelesIncidencia {get;set;}
        public DbSet<Incidencia> ? Incidencias {get;set;}
        public DbSet<TipoDocumento> ? TiposDocumento {get; set;}
        public DbSet<DetalleIncidencia> ? DetallesIncidencia {get;set;}
        public DbSet<Contacto> ? Contactos { get; set; }
        public DbSet<CategoriaContacto> ? CategoriaContactos { get; set; }
        public DbSet<AreaUsuario> ? AreaUsuarios { get; set; }
        public DbSet<Area> ? Areas { get; set; }
        public DbSet<Direccion> ? Direcciones { get; set; }
        public DbSet<Ciudad> ? Ciudades { get; set; }
        public DbSet<Region> ? Regiones { get; set; }
        public DbSet<Pais> ? Paises { get; set; }
        public DbSet<TipoContacto> TipoContactos {get; set;}
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(idx => idx.Username).IsUnique();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
    
    
}