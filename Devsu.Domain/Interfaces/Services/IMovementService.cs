using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;


namespace Devsu.Domain.Interfaces.Services
{
    public interface IMovementService
    {
        public Task<IList<Movimiento>> GetAllAsync();

        public Task<Movimiento> GetByIdAsync(long Id);

        public Task<Movimiento> AddAsync(Movimiento entity);
        public Task<Movimiento> UpdateAsync(long id, Movimiento entity);
        public Task<Movimiento> PatchAsync(long id, JsonPatchDocument entity);
        public Task<Movimiento> DeleteAsync(long id);
    }
}
