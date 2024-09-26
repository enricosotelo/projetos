using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Models;

public interface IMedicoRepository
{
    Task<Medico> GetAsync(string id);
    Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade);
    Task<bool> VerifyAvailabilityAsync(string id, DateTime data);
}