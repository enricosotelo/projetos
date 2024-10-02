using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaMed.Repositories.Interfaces
{
    public interface IMedicoRepository
    {
        Task<Medico> GetAsync(string id);
        Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade);
        Task<bool> VerifyAvailabilityAsync(string id, DateTime data);
        Task<IEnumerable<Medico>> GetAsync();
        Task CreateAsync(Medico medico);
   



    }
}