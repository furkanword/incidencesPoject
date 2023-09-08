using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Dominio.Interfaces
{
    public interface IRolRepository : IGenericRepository<Rol> 
    {
        Task<Rol> GetRolByName(string Name);
    }
}