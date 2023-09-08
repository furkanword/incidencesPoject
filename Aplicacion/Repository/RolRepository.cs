using Dominio.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    public RolRepository(ApiContext context) : base(context)
    {  
        
    }

    public async Task<Rol> GetRolByName(string name)
    {
        return await _context.Roles!.Where(x => x.Name_Rol == name).FirstAsync();
    }
}
