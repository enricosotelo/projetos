using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.DataContext;

namespace AgendaMed.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AgendaMedDbContext _context;

        public PacienteRepository(AgendaMedDbContext context)
        {
            _context = context;
        }

        public async Task<Paciente> GetAsync(string id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<Paciente> GetByEmailAsync(string email)
        {
            return await _context.Pacientes.SingleOrDefaultAsync(p => p.Email == email);
        }

        public async Task ActivateAsync(string id)
        {
            var paciente = await GetAsync(id);
            if (paciente != null)
            {
                paciente.Ativo = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateAsync(string id)
        {
            var paciente = await GetAsync(id);
            if (paciente != null)
            {
                paciente.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Paciente>> GetAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task CreateAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }
    }
}