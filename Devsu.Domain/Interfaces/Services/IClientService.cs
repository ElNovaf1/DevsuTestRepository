using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;

namespace Devsu.Domain.Interfaces.Services
{
    public interface IClientService
    {
        //public Task<IList<Cliente>> GetAllAsync();

        public Task<Cliente> GetByIdAsync(int Id);

        public Task<ReporteMovimientosClienteDTO> GetReporteMovimientosAsync(int id, DateTime? finicio, DateTime? ffin);
        public Task<Cliente> AddAsync(Cliente entity);
        public Task<Cliente> UpdateAsync(int id, Cliente entity);
        public Task<Cliente> PatchAsync(int id,JsonPatchDocument entity);
        public Task<Cliente> DeleteAsync(int id);
    }
}
