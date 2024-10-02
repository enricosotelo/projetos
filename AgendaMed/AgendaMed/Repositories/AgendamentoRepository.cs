using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.DataContext;
using System.Collections.Generic;
using System.Linq;
using AgendaMed.Repositories.Interfaces;

namespace AgendaMed.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AgendaMedDbContext _context;

        public AgendamentoRepository(AgendaMedDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Agendamento>> GetAsync()
        {
            return await _context.Agendamentos
                .Include(a => a.Paciente)
                .Include(a => a.Medico) 
                .ToListAsync();
        }

        public async Task<Agendamento> GetAsync(string id)
        {
            return await _context.Agendamentos.FindAsync(id);
        }

        public async Task<Agendamento> CreateAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<Agendamento> UpdateAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Update(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task DeleteAsync(string id)
        {
            var agendamento = await GetAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}