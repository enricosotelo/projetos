using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.DataContext;
using AgendaMed.Repositories.Interfaces;

namespace AgendaMed.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly AgendaMedDbContext _context;

        public MedicoRepository(AgendaMedDbContext context)
        {
            _context = context;
        }

        public async Task<Medico> GetAsync(string id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        public async Task<IEnumerable<Medico>> GetByEspecialidadeAsync(string especialidade)
        {
            return await _context.Medicos.Where(m => m.Especialidade == especialidade).ToListAsync();
        }

        public async Task<bool> VerifyAvailabilityAsync(string id, DateTime data)
        {
            var agendamentos = await _context.Agendamentos
                                     .Where(a => a.MedicoId == id && a.Date == data)
                                     .ToListAsync();
            return !agendamentos.Any();
        }

        public async Task<IEnumerable<Medico>> GetAsync()
        {
            return await _context.Medicos.ToListAsync();
        }

        public async Task CreateAsync(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
        }
    }
}