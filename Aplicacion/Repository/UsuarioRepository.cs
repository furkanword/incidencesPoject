using Dominio;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly ApiContext _context;

    public UsuarioRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
        public async Task<Usuario> GetByUsernameAsync(string username)
    {   
        return( await _context.Usuarios!.Include(u => u.UsuariosRoles)
                            .Where(u => u.Username!.ToLower() == username.ToLower())
                            .FirstOrDefaultAsync())!;
    }
}
