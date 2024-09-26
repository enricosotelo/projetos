using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.DataContext;
using AgendaMed.Models;

public class MedicoRepository : IMedicoRepository
{
    private readonly DbContext _context;

    public MedicoRepository(DbContext context)
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
        // TO DO: implement logic to verify medico availability
        // for now, return true
        return true;
    }
}