using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.DataContext;
using AgendaMed.Models;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly DbContext _context;

    public AgendamentoRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<Agendamento> CreateAsync(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }
}