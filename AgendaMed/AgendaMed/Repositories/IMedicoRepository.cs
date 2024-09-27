using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.Models;

namespace AgendaMed.Repositories
{
    public interface IMedicoRepository
    {
        Task<Medico> GetAsync(string id);
        Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade);
        Task<bool> VerifyAvailabilityAsync(string id, DateTime data);
        Task<IEnumerable<Medico>> GetAsync();
        Task CreateAsync(Medico medico);
        Task UpdateAsync(Medico medico);
        Task DeleteAsync(Medico medico);
    }
}