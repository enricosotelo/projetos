using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Models;

public interface IAgendamentoRepository
{
    Task<Agendamento> CreateAsync(Agendamento agendamento);
}