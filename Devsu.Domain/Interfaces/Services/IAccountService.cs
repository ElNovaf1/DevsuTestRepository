using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;

namespace Devsu.Domain.Interfaces.Services
{
    public  interface IAccountService
    {
        public Task<IList<Cuenta>> GetAllAsync();

        public Task<Cuenta> GetByIdAsync(int Id);

        public Task<Cuenta>  GetMovementsByIdAsync(int Id);

        public Task<Cuenta> AddAsync(Cuenta entity);
        public Task<Cuenta> UpdateAsync(int id, Cuenta entity);
        public Task<Cuenta> PatchAsync(int id, JsonPatchDocument entity);
        public Task<Cuenta> DeleteAsync(int id);
    }
}
