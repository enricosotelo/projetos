using System.Threading.Tasks;
using AgendaMed.Models;
using System.Collections.Generic;

namespace AgendaMed.Repositories
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> GetAsync();
        Task<Agendamento> GetAsync(string id);
        Task<Agendamento> CreateAsync(Agendamento agendamento);
        Task<Agendamento> UpdateAsync(Agendamento agendamento);
        Task DeleteAsync(string id);
    }
}