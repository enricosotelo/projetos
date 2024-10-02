using AgendaMed.Models;
using AgendaMed.DTO;

namespace AgendaMed.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> GetAgendamentosAsync();
        Task<Agendamento> GetAgendamentoByIdAsync(string id);
        Task<Agendamento> CreateAgendamentoAsync(string idMedico, string idPaciente, DateTime data);
        Task<Agendamento> CreateAgendamentoAsync(AgendamentoDTO agendamentoDTO);
        Task<Agendamento> UpdateAgendamentoAsync(string id, Agendamento agendamento);
        Task DeleteAgendamentoAsync(string id);

        Task<Agendamento> AgendarMedicoAleatoriamente(string especialidade, AgendamentoEspecialidadeDTO request);

    }
}