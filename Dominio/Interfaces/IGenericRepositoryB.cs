using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGenericRepositoryB<T>{
        Task<T> GetByStringIdAsync(int id);        
    }
}