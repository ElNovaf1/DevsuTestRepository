using Devsu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        public Task<IList<Persona>> GetAllAsync();

        public Task<Persona> GetByIdAsync(int Id);
        public Task<IList<Persona>> GetByFilterAsync(Expression<Func<Persona, bool>> filter, params Expression<Func<Persona, object>>[] includeExpressions);
        public Task<Persona> AddAsync(Persona entity);
        public Task<Persona> UpdateAsync(Persona entity);
        public void DeleteAsync(int id);
    }
}
