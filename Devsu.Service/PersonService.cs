using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Devsu.Domain.Entities;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;


namespace Devsu.Service
{
    public class PersonService: IPersonService
    {
        private readonly IWorkUnit _WorkUnit;
        public PersonService(IWorkUnit workUnit)
        {
            _WorkUnit = workUnit;
        }

        public async Task<IList<Persona>> GetAllAsync() {
            return await _WorkUnit.Repository<Persona>().GetAllAsync();
        }

        public async Task<Persona> GetByIdAsync(int Id) {
            return await _WorkUnit.Repository<Persona>().GetByIdAsync(Id);
        }
        public async Task<IList<Persona>> GetByFilterAsync(Expression<Func<Persona, bool>> filter, params Expression<Func<Persona, object>>[] includeExpressions)
        {
            return await _WorkUnit.Repository<Persona>().GetByFilterAsync(filter,includeExpressions);
        }
        public async Task<Persona> AddAsync(Persona entity) {
            return await _WorkUnit.Repository<Persona>().AddAsync(entity);
        }
        public async Task<Persona> UpdateAsync(Persona entity) {
            return await _WorkUnit.Repository<Persona>().UpdateAsync(entity);
        }
        public async void DeleteAsync(int id) {
            await _WorkUnit.Repository<Persona>().DeleteAsync(id);
        }



    }
}
